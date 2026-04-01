// -----------------------------------------------------------------------
// <copyright file= "AIProviderConfiguration.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-04-01 21:25
// Modified by:
// Description: AI Provider Entity Configuration
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryInsights.Domain.Enums;
using SalaryInsights.Domain.Models;

namespace SalaryInsights.Infrastructure.Configurations;

public class AIProviderConfiguration : IEntityTypeConfiguration<AIProvider>
{
    public void Configure(EntityTypeBuilder<AIProvider> builder)
    {
        builder.ToTable("ai_providers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.AIProviderType)
            .HasColumnName("ai_provider_type")
            .HasConversion(
                v => v.ToString().ToLower(),
                v => Enum.Parse<AIProviderTypes>(v, true))
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(x => x.Endpoint)
            .HasColumnName("endpoint")
            .HasMaxLength(500);

        builder.Property(x => x.APIKey)
            .HasColumnName("api_key")
            .HasMaxLength(500);
        
        builder.Property(x => x.IsDefault)
            .HasColumnName("is_default")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasMany(x => x.Models)
            .WithOne(x => x.AIProvider)
            .HasForeignKey(x => x.AIProviderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
