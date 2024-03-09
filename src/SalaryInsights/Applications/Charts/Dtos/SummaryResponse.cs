// -----------------------------------------------------------------------
// <copyright file= "SummaryResponse.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-3-9 15:37
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Applications.Charts.Dtos;

public class SummaryResponse
{
    public decimal TotalIncome { get; set; }
    
    public decimal YearlyIncome { get; set; }
    
    public decimal MonthlyIncome { get; set; }
}