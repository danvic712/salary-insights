// -----------------------------------------------------------------------
// <copyright file= "SalaryConfiguration.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-12 22:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryInsights.Domain.Models;

namespace SalaryInsights.Infrastructure.EntityFrameworkCore.Configurations;

public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.ToTable("salaries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.GrossIncome)
            .HasColumnType("numeric(18,2)");

        builder.Property<object>(x => x.NetIncome)
            .HasColumnType("numeric(18,2)");

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(x => new { x.PeriodStart });
    }
}
