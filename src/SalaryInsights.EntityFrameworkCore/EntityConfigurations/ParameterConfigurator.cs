// -----------------------------------------------------------------------
// <copyright file= "ParameterConfigurator.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 16:24
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryInsights.EntityFrameworkCore.Models;

namespace SalaryInsights.EntityFrameworkCore.EntityConfigurations;

public class ParameterConfigurator : IEntityTypeConfiguration<Parameter>
{
    public void Configure(EntityTypeBuilder<Parameter> builder)
    {
        builder.ToTable("tbl_parameters");

        builder.HasKey(i => i.Id)
            .HasName("pk_id");

        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        builder.Property(i => i.ParameterType)
            .IsRequired()
            .HasColumnType("INTEGER")
            .HasColumnName("parameter_type");
        
        builder.Property(i => i.Name)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(i => i.Description)
            .IsRequired(false)
            .HasColumnName("description");
    }
}