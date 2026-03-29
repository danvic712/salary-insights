// -----------------------------------------------------------------------
// <copyright file= "IMultiTenant.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 20:02
// Modified by:
// Description: Multi-tenant interface
// -----------------------------------------------------------------------

namespace SalaryInsights.Domain.Contracts;

public interface IMultiTenant
{
    /// <summary>
    /// TenantId
    /// </summary>
    Guid TenantId { get; }
}