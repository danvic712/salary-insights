// -----------------------------------------------------------------------
// <copyright file= "IChartManager.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-3-6 22:13
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Applications.Charts.Dtos;

namespace SalaryInsights.Applications.Charts.Contracts;

public interface IChartManager
{
    Task<SummaryResponse> GetSummaryAsync();

    Task<IList<YearlyTrendsResponse>> GetYearlyTrendsAsync(int limit);

    Task<IList<MonthlyTrendsResponse>> GetMonthlyTrendsAsync(int year);
}