// -----------------------------------------------------------------------
// <copyright file= "Payroll.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 19:53
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Models;

public class Payroll
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public Guid Id { get; set; }
    
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
    public decimal GrossPay { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public decimal NetPay { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    public string? Remark { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Guid UserId { get; set; }
}