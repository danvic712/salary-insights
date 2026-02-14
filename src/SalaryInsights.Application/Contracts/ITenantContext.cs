// -----------------------------------------------------------------------
// <copyright file= "ITenantContext.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 20:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Application.Contracts;

public interface ITenantContext
{
    /// <summary>
    /// TenantId
    /// </summary>
    Guid UserId { get; }
    
    /// <summary>
    /// TenantId
    /// </summary>
    Guid? CurrentTenantId { get; }
    
    /// <summary>
    /// Is Super Admin
    /// </summary>
    bool IsSuperAdmin { get; }
}