// -----------------------------------------------------------------------
// <copyright file= "GridRequest.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-04-11 21:04
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Applications.Shared.Dtos;

public class GridRequest
{
    /// <summary>
    /// 
    /// </summary>
    public string Filters { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string Sort { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// 
    /// </summary>
    public int PageSize { get; set; } = 20;
}