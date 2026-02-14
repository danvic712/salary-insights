// -----------------------------------------------------------------------
// <copyright file= "SalaryInsightsDbContext.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-12 22:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SalaryInsights.Application.Contracts;
using SalaryInsights.Domain.Contracts;
using SalaryInsights.Domain.Models;

namespace SalaryInsights.Infrastructure;

public class SalaryInsightsDbContext(
    DbContextOptions<SalaryInsightsDbContext> options,
    ITenantContext tenantContext) : DbContext(options)
{
    public DbSet<Salary> Salaries => Set<Salary>();
    public DbSet<SalaryItem> SalaryItems => Set<SalaryItem>();
    public DbSet<Company> Companies => Set<Company>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("salary");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalaryInsightsDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(IMultiTenant).IsAssignableFrom(entityType.ClrType))
                continue;

            var method = typeof(SalaryInsightsDbContext)
                .GetMethod(nameof(SetTenantFilter), BindingFlags.NonPublic | BindingFlags.Instance)!
                .MakeGenericMethod(entityType.ClrType);

            method.Invoke(this, [modelBuilder]);
        }
    }

    private void SetTenantFilter<TEntity>(ModelBuilder modelBuilder)
        where TEntity : class, IMultiTenant
    {
        modelBuilder.Entity<TEntity>()
            .HasQueryFilter(e =>
                tenantContext.CurrentTenantId == null || e.TenantId == tenantContext.CurrentTenantId);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<IMultiTenant>())
        {
            if (entry.State != EntityState.Added)
                continue;

            entry.Property("TenantId").CurrentValue = tenantContext.CurrentTenantId ?? tenantContext.UserId;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}