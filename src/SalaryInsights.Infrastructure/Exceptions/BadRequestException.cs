// -----------------------------------------------------------------------
// <copyright file= "BadRequestException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 22:25
// Modified by:
// Description: Bad request exception
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace SalaryInsights.Infrastructure.Exceptions;

public sealed class BadRequestException(string message, string errorCode = "bad_request")
    : SIException(message, StatusCodes.Status400BadRequest, errorCode)
{
    public override string Title => "Bad Request";
}
