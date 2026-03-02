// -----------------------------------------------------------------------
// <copyright file= "IDatabaseMigrator.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-25 22:50
// Modified by:
// Description: Database migration interface
// -----------------------------------------------------------------------

namespace SalaryInsights.Application.Contracts;

public interface IDatabaseMigrator
{
    /// <summary>
    /// Execute database migrations and seed data
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task MigrateAsync(CancellationToken cancellationToken = default);
}