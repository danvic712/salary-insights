// -----------------------------------------------------------------------
// <copyright file= "Company.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 20:42
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.EntityFrameworkCore.Models;

public class Company
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }
}