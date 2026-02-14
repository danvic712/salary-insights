// -----------------------------------------------------------------------
// <copyright file= "UnauthorizedException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 23:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace SalaryInsights.Infrastructure.Exceptions;

public sealed class UnauthorizedException(string message, string errorCode = "unauthorized")
    : SIException(message, StatusCodes.Status401Unauthorized, errorCode)
{
    public override string Title => "Unauthorized";
}