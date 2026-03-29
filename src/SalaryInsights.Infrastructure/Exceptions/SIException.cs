// -----------------------------------------------------------------------
// <copyright file= "SIException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 23:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Infrastructure.Exceptions;

public abstract class SIException(
    string message,
    int statusCode,
    string errorCode,
    Exception? innerException = null) : Exception(message, innerException)
{
    public int StatusCode { get; } = statusCode;

    public string ErrorCode { get; } = errorCode;

    public virtual string Title => "Business Error";
}