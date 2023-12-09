// -----------------------------------------------------------------------
// <copyright file= "SalaryItemType.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 20:48
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.EntityFrameworkCore.Models;

public class SalaryItemType
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 类型名称
    /// </summary>
    public string Name { get; set; }
}