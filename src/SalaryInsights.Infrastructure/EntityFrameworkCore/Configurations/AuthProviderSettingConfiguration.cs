// -----------------------------------------------------------------------
// <copyright file= "AuthProviderSettingConfiguration.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-27 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryInsights.Domain.Models;

namespace SalaryInsights.Infrastructure.EntityFrameworkCore.Configurations;

public class AuthProviderSettingConfiguration
    : IEntityTypeConfiguration<AuthProviderSetting>
{
    public void Configure(EntityTypeBuilder<AuthProviderSetting> builder)
    {
        builder.ToTable("auth_provider_settings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Setting)
            .HasColumnName("settings")
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(x => x.IsInitialized)
            .HasColumnName("is_initialized");
        
        builder.HasIndex(x => x.AuthProviderId)
            .IsUnique();
    }
}