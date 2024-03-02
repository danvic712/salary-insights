// -----------------------------------------------------------------------
// <copyright file= "PayrollDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 20:27
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Dtos;

public class PayrollDto
{
    
    /// <summary>
    /// Payroll id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Associated company id
    /// </summary>
    public Guid CompanyId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    /// 月份
    /// </summary>
    public DateTime Month { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public decimal GrossSalary { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public decimal NetSalary { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    public string? Remark { get; set; }
}