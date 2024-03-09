// -----------------------------------------------------------------------
// <copyright file= "ResponseBase.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 13:0
// Modified by:
// Description: Standard API response object
// -----------------------------------------------------------------------

namespace SalaryInsights.Applications.Shared.Dtos;

public class ResponseBase<T> where T : class
{
    #region Properties

    /// <summary>
    /// Request status
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// Response data
    /// </summary>
    public T Resource { get; set; } = default;

    /// <summary>
    /// Error information
    /// </summary>
    public Dictionary<string, IList<string>> Errors { get; set; } = new();

    #endregion Properties
}