// -----------------------------------------------------------------------
// <copyright file= "SalaryItem.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 20:45
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.EntityFrameworkCore.Models;

public class SalaryItem
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid PayrollId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid SalaryItemTypeId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool Income { get; set; }
}