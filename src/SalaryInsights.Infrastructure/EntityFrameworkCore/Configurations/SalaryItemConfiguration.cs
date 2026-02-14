// -----------------------------------------------------------------------
// <copyright file= "SalaryItemConfiguration.cs">
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

public class SalaryItemConfiguration : IEntityTypeConfiguration<SalaryItem>
{
    public void Configure(EntityTypeBuilder<SalaryItem> builder)
    {
        builder.ToTable("salary_items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasColumnType("numeric(18,2)");

        builder.Property(x => x.Type)
            .HasConversion<short>();

        builder.HasIndex(x => x.SalaryId);
    }
}