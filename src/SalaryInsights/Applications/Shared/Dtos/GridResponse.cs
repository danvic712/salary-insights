// -----------------------------------------------------------------------
// <copyright file= "GridResponse.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 12:59
// Modified by:
// Description: Standard API pagination response object
// -----------------------------------------------------------------------

namespace SalaryInsights.Applications.Shared.Dtos;

public class GridResponse<T>
{
    #region Properties

    /// <summary>
    /// Grid summary
    /// </summary>
    public Dictionary<string, string> Summary { get; set; } = new();
    
    /// <summary>
    /// Current Page
    /// </summary>
    public int CurrentPage { get; set; }

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