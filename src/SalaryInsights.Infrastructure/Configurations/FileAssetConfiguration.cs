// -----------------------------------------------------------------------
// <copyright file= "FileAssetConfiguration.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 17:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryInsights.Domain.Enums;
using SalaryInsights.Domain.Models;

namespace SalaryInsights.Infrastructure.Configurations;

public class FileAssetConfiguration : IEntityTypeConfiguration<FileAsset>
{
    public void Configure(EntityTypeBuilder<FileAsset> builder)
    {
        builder.ToTable("file_assets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.FileName)
            .HasColumnName("file_name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.FileSize)
            .HasColumnName("file_size")
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasColumnName("content_type")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.FileHash)
            .HasColumnName("file_hash")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(x => x.FileHash);

        builder.Property(x => x.StorageType)
            .HasColumnName("storage_type")
            .HasConversion(
                v => v.ToString().ToLower(),
                v => Enum.Parse<StorageTypes>(v, true))
            .IsRequired();

        builder.HasIndex(x => x.StorageType);

        builder.Property(x => x.FilePath)
            .HasColumnName("file_path")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
    }
}