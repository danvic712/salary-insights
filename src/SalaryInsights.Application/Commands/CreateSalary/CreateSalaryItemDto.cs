// -----------------------------------------------------------------------
// <copyright file= "CreateSalaryItemDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-12 22:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Domain.Enums;

namespace SalaryInsights.Application.Commands.CreateSalary;

public record CreateSalaryItemDto(
    string Name,
    SalaryItemTypes Type,
    decimal Amount
);