// -----------------------------------------------------------------------
// <copyright file= "DatabaseMigrator.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-25 22:50
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SalaryInsights.Application.Contracts;
using SalaryInsights.Infrastructure.Contracts;

namespace SalaryInsights.Infrastructure;

public class DatabaseMigrator(
    IServiceProvider serviceProvider,
    IEnumerable<IDataSeeder> seeders,
    ILogger<DatabaseMigrator> logger) : IDatabaseMigrator
{
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Starting database migration...");

            var dbContext = serviceProvider.GetRequiredService<SalaryInsightsDbContext>();

            await dbContext.Database.EnsureCreatedAsync(cancellationToken);

            await dbContext.Database.MigrateAsync(cancellationToken);

            logger.LogInformation("Database migration completed successfully");

            logger.LogInformation("Starting data seeding...");

            foreach (var seeder in seeders.OrderBy(i => i.Order))
            {
                logger.LogInformation("Running seeder: {SeederName}", seeder.GetType().Name);
                await seeder.SeedAsync(cancellationToken);
            }

            logger.LogInformation("Data seeding completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to run database migrations and seeders");
        }
    }
}