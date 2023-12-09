// -----------------------------------------------------------------------
// <copyright file= "Payroll.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 19:53
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.EntityFrameworkCore.Models;

public class Payroll
{
    public Payroll()
    {
        SalaryItems = new HashSet<SalaryItem>();
    }
    
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Guid CompanyId { get; set; }

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

    // 备注
    public string Remarks { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual Company Company { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<SalaryItem> SalaryItems { get; set; }
}