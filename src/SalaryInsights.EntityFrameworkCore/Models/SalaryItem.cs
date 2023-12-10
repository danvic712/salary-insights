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
    public Guid Id { get; set; }

    public Guid PayrollId { get; set; }

    public Guid SalaryItemTypeId { get; set; }

    public decimal Amount { get; set; }

    public string? Remark { get; set; }

    public virtual Payroll Payroll { get; set; }

    public virtual Parameter SalaryItemType { get; set; }
}