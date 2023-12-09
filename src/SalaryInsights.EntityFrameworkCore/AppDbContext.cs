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
using Microsoft.Extensions.Configuration;
using SalaryInsights.EntityFrameworkCore.Models;

namespace SalaryInsights.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Company> Companies { get; set; }

    public DbSet<Payroll> Payrolls { get; set; }

    public DbSet<SalaryItem> SalaryItems { get; set; }

    public DbSet<SalaryItemType> SalaryItemTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("SalaryInsights"));
    }
}