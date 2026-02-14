// -----------------------------------------------------------------------
// <copyright file= "SalaryItem.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-11 22:02
// Modified by:
// Description: Salary item entity model
// -----------------------------------------------------------------------

using SalaryInsights.Domain.Enums;

namespace SalaryInsights.Domain.Models;

public class SalaryItem
{
    public Guid Id { get; private set; }

    public Guid SalaryId { get; private set; }

    public SalaryItemTypes Type { get; private set; }

    public string Name { get; private set; } = null!;

    public decimal Amount { get; private set; }

    public decimal? Confidence { get; private set; }

    private SalaryItem()
    {
    }

    public SalaryItem(Guid salaryId, string name, SalaryItemTypes type, decimal amount)
    {
        Id = Guid.NewGuid();
        SalaryId = salaryId;
        Name = name;
        Type = type;
        Amount = amount;
    }
}