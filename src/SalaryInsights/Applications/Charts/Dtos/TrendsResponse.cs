// -----------------------------------------------------------------------
// <copyright file= "TrendsResponse.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-3-9 17:21
// Modified by:
// Description: Income trending chart
// -----------------------------------------------------------------------

namespace SalaryInsights.Applications.Charts.Dtos;

public class MonthlyTrendsResponse
{
    public DateTime Month { get; set; }

    public decimal GrossPay { get; set; }

    public decimal NetPay { get; set; }
}

public class YearlyTrendsResponse
{
    public int Year { get; set; }

    public decimal GrossPay { get; set; }

    public decimal NetPay { get; set; }
}