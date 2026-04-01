// -----------------------------------------------------------------------
// <copyright file= "AIModelConfiguration.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-04-01 21:25
// Modified by:
// Description: AI Model Entity Configuration
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryInsights.Domain.Models;

namespace SalaryInsights.Infrastructure.Configurations;

public class AIModelConfiguration : IEntityTypeConfiguration<AIModel>
{
    public void Configure(EntityTypeBuilder<AIModel> builder)
    {
        builder.ToTable("ai_models");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.AIProviderId)
            .HasColumnName("ai_provider_id")
            .IsRequired();

        builder.Property(x => x.DeploymentName)
            .HasColumnName("deployment_name")
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(x => x.ExtraInfo)
            .HasColumnName("extra_info")
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasOne(x => x.AIProvider)
            .WithMany(x => x.Models)
            .HasForeignKey(x => x.AIProviderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}