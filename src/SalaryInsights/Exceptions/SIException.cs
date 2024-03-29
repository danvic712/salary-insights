// -----------------------------------------------------------------------
// <copyright file= "SIException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-2-3 19:10
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Exceptions;

public class SIException : Exception
{
    public string Message { get; set; }
}