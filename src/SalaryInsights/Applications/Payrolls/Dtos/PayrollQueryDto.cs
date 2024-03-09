// -----------------------------------------------------------------------
// <copyright file= "PayrollQueryDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-1-20 19:55
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Applications.Payrolls.Dtos;

public class PayrollQueryDto
{
    /// <summary>
    /// 
    /// </summary>
    public DateTime? StartTime { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public  DateTime? EndTime { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? CompanyName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int Page { get; set; } = 1;
    
    /// <summary>
    /// 
    /// </summary>
    public int PageSize { get; set; } = 15;
}