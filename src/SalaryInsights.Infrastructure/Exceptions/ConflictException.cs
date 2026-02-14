// -----------------------------------------------------------------------
// <copyright file= "ConflictException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 23:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace SalaryInsights.Infrastructure.Exceptions;

public sealed class ConflictException(string message, string errorCode = "conflict")
    : SIException(message, StatusCodes.Status409Conflict, errorCode)
{
    public override string Title => "Conflict";
}