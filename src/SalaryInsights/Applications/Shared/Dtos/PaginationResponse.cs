// -----------------------------------------------------------------------
// <copyright file= "PaginationResponse.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 12:59
// Modified by:
// Description: Standard API pagination response object
// -----------------------------------------------------------------------

namespace SalaryInsights.Applications.Shared.Dtos;

public class PaginationResponse<T> : ResponseBase<PaginationResource<T>>
{
}

public class PaginationResource<T>
{
    #region Properties

    /// <summary>
    /// Total data
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Paging data
    /// </summary>
    public IList<T> Data { get; set; } = new List<T>();

    #endregion Properties
}