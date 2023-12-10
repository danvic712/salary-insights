// -----------------------------------------------------------------------
// <copyright file= "SalaryItemConfigurator.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 23:11
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryInsights.EntityFrameworkCore.Models;

namespace SalaryInsights.EntityFrameworkCore.EntityConfigurations;

public class SalaryItemConfigurator : IEntityTypeConfiguration<SalaryItem>
{
    public void Configure(EntityTypeBuilder<SalaryItem> builder)
    {
        builder.ToTable("tbl_salary_items");

        builder.HasKey(i => i.Id)
            .HasName("pk_id");

        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        builder.Property(i => i.PayrollId)
            .IsRequired()
            .HasColumnName("payroll_id");

        builder.Property(i => i.SalaryItemTypeId)
            .IsRequired()
            .HasColumnName("salary_item_type_id");
        
        builder.Property(i => i.Amount)
            .HasColumnType("NUMERIC")
            .HasColumnName("amount");
        
        builder.Property(i => i.Remark)
            .IsRequired(false)
            .HasColumnName("remark");
        
        builder.HasOne(i => i.SalaryItemType)
            .WithMany()
            .HasForeignKey(si => si.SalaryItemTypeId); 
    }
}