// -----------------------------------------------------------------------
// <copyright file= "ChartsController.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-3-6 22:7
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using SalaryInsights.Applications.Charts.Contracts;
using SalaryInsights.Applications.Charts.Dtos;

namespace SalaryInsights.Controllers;

/// <summary>
/// Company resource endpoints
/// </summary>
[ApiController]
[Route("api/charts")]
public class ChartsController : ControllerBase
{
    #region Initializes

    private readonly IChartManager _chartManager;

    public ChartsController(IChartManager chartManager)
    {
        _chartManager = chartManager;
    }

    #endregion

    #region APIs

    /// <summary>
    /// Returns the income summary for the current user
    /// </summary>
    /// <returns></returns>
    [HttpGet("summary")]
    public async Task<SummaryResponse> GetSummaryAsync()
    {
        return await _chartManager.GetSummaryAsync();
    }

    /// <summary>
    /// Returns the income trends for every year
    /// </summary>
    /// <param name="limit"></param>
    /// <returns></returns>
    [HttpGet("years")]
    public async Task<IList<YearlyTrendsResponse>> GetYearlyTrendsAsync([FromQuery] int limit = 6)
    {
        return await _chartManager.GetYearlyTrendsAsync(limit);
    }

    /// <summary>
    /// Returns the income trends for the provided year
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    [HttpGet("years/{year}")]
    public async Task<IList<MonthlyTrendsResponse>> GetMonthlyTrendsAsync([FromRoute] int year)
    {
        return await _chartManager.GetMonthlyTrendsAsync(year);
    }

    #endregion
}