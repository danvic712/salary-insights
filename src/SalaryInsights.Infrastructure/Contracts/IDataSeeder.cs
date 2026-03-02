// -----------------------------------------------------------------------
// <copyright file= "IDataSeeder.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-27 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Infrastructure.Contracts;

public interface IDataSeeder
{
    long Order => 0;

    Task SeedAsync(CancellationToken cancellationToken = default);
}