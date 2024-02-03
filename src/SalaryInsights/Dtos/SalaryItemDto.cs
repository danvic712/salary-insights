// -----------------------------------------------------------------------
// <copyright file= "SalaryItemDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-2-3 18:35
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Dtos;

public class SalaryItemDto
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid SalaryItemTypeId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string SalaryItemType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    public string? Remark { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool Positive { get; set; }
}