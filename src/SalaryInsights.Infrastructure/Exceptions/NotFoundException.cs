// -----------------------------------------------------------------------
// <copyright file= "NotFoundException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 23:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace SalaryInsights.Infrastructure.Exceptions;

public sealed class NotFoundException(string message, string errorCode = "not_found")
    : SIException(message, StatusCodes.Status404NotFound, errorCode)
{
    public override string Title => "Not Found";
}