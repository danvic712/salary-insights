// -----------------------------------------------------------------------
// <copyright file= "SalaryItemCreationDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-2-12 14:15
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Applications.Payrolls.Dtos;

public class SalaryItemCreationDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid SalaryItemTypeId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    public string? Remark { get; set; }
    
    /// <summary>
    /// This item was income or expenditure
    /// </summary>
    public bool Income { get; set; }
}