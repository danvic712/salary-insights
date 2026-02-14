// -----------------------------------------------------------------------
// <copyright file= "Company.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-11 22:02
// Modified by:
// Description: Company entity model
// -----------------------------------------------------------------------

using SalaryInsights.Domain.Contracts;

namespace SalaryInsights.Domain.Models;

public class Company : IMultiTenant
{
    /// <summary>
    /// Identifier for the company
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Tenant identifier
    /// </summary>
    public Guid TenantId { get; private set; }

    /// <summary>
    /// Company name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    private Company()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="name"></param>
    public Company(Guid tenantId, string name)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
    }
}