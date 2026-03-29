// -----------------------------------------------------------------------
// <copyright file= "OperationResult.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-24 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Domain.Dtos;

public class OperationResult<T>
{
    public bool Success { get; set; }

    public string Message { get; set; } = "";

    public T? Data { get; set; }

    public static OperationResult<T> Ok(T? data = default, string message = "")
        => new OperationResult<T>
        {
            Success = true,
            Data = data,
            Message = message
        };

    public static OperationResult<T> Fail(string message, T? data = default)
        => new OperationResult<T>
        {
            Success = false,
            Message = message,
            Data = data
        };
}