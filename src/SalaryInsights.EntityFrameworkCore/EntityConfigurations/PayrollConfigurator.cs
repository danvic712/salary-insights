// -----------------------------------------------------------------------
// <copyright file= "PayrollConfigurator.cs">
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

public class PayrollConfigurator : IEntityTypeConfiguration<Payroll>
{
    public void Configure(EntityTypeBuilder<Payroll> builder)
    {
        builder.ToTable("tbl_payrolls");

        builder.HasKey(i => i.Id)
            .HasName("pk_id");

        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(i => i.CompanyId)
            .IsRequired()
            .HasColumnName("company_id");

        builder.Property(i => i.Month)
            .IsRequired()
            .HasColumnName("month");

        builder.Property(i => i.GrossSalary)
            .IsRequired()
            .HasColumnType("NUMERIC")
            .HasColumnName("gross_salary");

        builder.Property(i => i.NetSalary)
            .IsRequired()
            .HasColumnType("NUMERIC")
            .HasColumnName("net_salary");

        builder.Property(i => i.Remark)
            .IsRequired(false)
            .HasColumnName("remark");

        builder.HasOne(i => i.Company)
            .WithMany()
            .HasForeignKey(i => i.CompanyId);

        builder.HasMany(i => i.SalaryItems)
            .WithOne(i => i.Payroll)
            .HasForeignKey(i => i.PayrollId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}