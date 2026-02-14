// -----------------------------------------------------------------------
// <copyright file= "Salary.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-11 22:02
// Modified by:
// Description: Salary entity model
// -----------------------------------------------------------------------

using SalaryInsights.Domain.Contracts;
using SalaryInsights.Domain.Enums;

namespace SalaryInsights.Domain.Models;

public class Salary : IMultiTenant
{
    /// <summary>
    /// Identifier for the salary record
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Tenant identifier
    /// </summary>
    public Guid TenantId { get; private set; }

    /// <summary>
    /// Identifier for the company
    /// </summary>
    public Guid? CompanyId { get; private set; }

    /// <summary>
    /// Salary period start date
    /// </summary>
    public DateOnly PeriodStart { get; private set; }

    /// <summary>
    /// Salary period end date
    /// </summary>
    public DateOnly PeriodEnd { get; private set; }

    /// <summary>
    /// Gross income amount
    /// </summary>
    public decimal GrossIncome { get; private set; }

    /// <summary>
    /// Total deductions amount
    /// </summary>
    public decimal TotalDeductions { get; private set; }

    /// <summary>
    /// Net income amount
    /// </summary>
    public decimal NetIncome { get; private set; }

    /// <summary>
    /// Confidence score of this salary record
    /// </summary>
    public decimal ConfidenceScore { get; private set; }

    /// <summary>
    /// Datetime when this salary record is created
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    private readonly List<SalaryItem> _items = new();
    public IReadOnlyCollection<SalaryItem> Items => _items.AsReadOnly();

    private Salary()
    {
    }

    public Salary(
        Guid? companyId,
        DateOnly start,
        DateOnly end,
        decimal gross,
        decimal net)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        PeriodStart = start;
        PeriodEnd = end;
        GrossIncome = gross;
        NetIncome = net;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(string name, SalaryItemTypes type, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Item name cannot be null or whitespace.");

        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        var item = new SalaryItem(Id, name, type, amount);
        _items.Add(item);
    }
}