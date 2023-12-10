// -----------------------------------------------------------------------
// <copyright file= "PaginationResponse.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 12:59
// Modified by:
// Description: Standard API pagination response object
// -----------------------------------------------------------------------

namespace SalaryInsights.Shared.Responses;

public class PaginationResponse<T> : ResponseBase<PaginationResource<T>>
{
}

public class PaginationResource<T>
{
    #region Properties

    /// <summary>
    /// Total data
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// Current page
    /// </summary>
    public int Current { get; set; }

    /// <summary>
    /// Next page
    /// </summary>
    public int Next { get; set; }

    /// <summary>
    /// Paging data
    /// </summary>
    public IList<T> Data { get; set; } = new List<T>();

    #endregion Properties
}