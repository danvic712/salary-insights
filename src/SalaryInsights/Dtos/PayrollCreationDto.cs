// -----------------------------------------------------------------------
// <copyright file= "PayrollCreationDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-2-3 18:40
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Dtos;

public class PayrollCreationDto
{
    /// <summary>
    /// Associated company id
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// Month
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

    /// <summary>
    /// Salary items
    /// </summary>
    public IList<SalaryItemCreationDto> SalaryItems { get; set; } = new List<SalaryItemCreationDto>();
}