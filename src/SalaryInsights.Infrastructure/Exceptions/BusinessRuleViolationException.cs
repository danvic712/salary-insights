// -----------------------------------------------------------------------
// <copyright file= "BusinessRuleViolationException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 23:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace SalaryInsights.Infrastructure.Exceptions;

public sealed class BusinessRuleViolationException(string message, string errorCode = "business_rule_violation")
    : SIException(message, StatusCodes.Status422UnprocessableEntity, errorCode)
{
    public override string Title => "Business Rule Violation";
}