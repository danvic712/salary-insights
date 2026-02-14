// -----------------------------------------------------------------------
// <copyright file= "GlobalExceptionHandler.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 23:02
// Modified by:
// Description: Global exception handler
// -----------------------------------------------------------------------

using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SalaryInsights.Infrastructure.Exceptions;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        switch (exception)
        {
            // 1) FluentValidation -> 400
            case ValidationException validationException:
            {
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).Distinct().ToArray());

                var problem = new ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Failed",
                    Instance = httpContext.Request.Path,
                    Extensions =
                    {
                        ["traceId"] = traceId
                    }
                };

                httpContext.Response.StatusCode = problem.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
                return true;
            }
            // 2) Business errors -> 4xx/422/409/404...
            case SIException appException:
            {
                var problem = new ProblemDetails
                {
                    Status = appException.StatusCode,
                    Title = appException.Title,
                    Detail = appException.Message,
                    Instance = httpContext.Request.Path,
                    Extensions =
                    {
                        ["traceId"] = traceId,
                        ["errorCode"] = appException.ErrorCode
                    }
                };

                httpContext.Response.StatusCode = problem.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
                return true;
            }
        }

        // 3) Unhandled errors -> 500
        logger.LogError(exception, "Unhandled exception. TraceId={TraceId}", traceId);

        var generic = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Internal Error",
            Detail = "Unexpected error occurred, please try again later.",
            Instance = httpContext.Request.Path,
            Extensions =
            {
                ["traceId"] = traceId
            }
        };

        httpContext.Response.StatusCode = generic.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(generic, cancellationToken);
        return true;
    }
}