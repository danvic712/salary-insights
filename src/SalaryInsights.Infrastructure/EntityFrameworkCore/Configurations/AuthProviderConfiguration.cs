// -----------------------------------------------------------------------
// <copyright file= "AuthProviderConfiguration.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-27 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryInsights.Domain.Enums;
using SalaryInsights.Domain.Models;

namespace SalaryInsights.Infrastructure.EntityFrameworkCore.Configurations;

public class AuthProviderConfiguration : IEntityTypeConfiguration<AuthProvider>
{
    public void Configure(EntityTypeBuilder<AuthProvider> builder)
    {
        builder.ToTable("auth_providers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProviderType)
            .HasColumnName("provider_type")
            .HasConversion(
                v => v.ToString().ToLower(),
                v => Enum.Parse<AuthProviderTypes>(v, true))
            .IsRequired();

        builder.HasIndex(x => x.ProviderType);

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active");

        builder.HasOne(x => x.ProviderSetting)
            .WithOne(x => x.AuthProvider)
            .HasForeignKey<AuthProviderSetting>(x => x.AuthProviderId);
    }
}