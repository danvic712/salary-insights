// -----------------------------------------------------------------------
// <copyright file= "AppDbContext.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 21:18
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SalaryInsights.EntityFrameworkCore.Models;

namespace SalaryInsights.EntityFrameworkCore;

public class SalaryInsightsDbContext : DbContext
{
    public SalaryInsightsDbContext(
        DbContextOptions<SalaryInsightsDbContext> options) : base(options)
    {
    }

    public DbSet<Parameter> Parameters { get; set; }

    public DbSet<Payroll> Payrolls { get; set; }

    public DbSet<SalaryItem> SalaryItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}