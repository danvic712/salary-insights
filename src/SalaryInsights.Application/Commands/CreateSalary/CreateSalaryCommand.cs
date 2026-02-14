// -----------------------------------------------------------------------
// <copyright file= "CreateSalaryCommand.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-12 22:02
// Modified by:
// Description: 
// -----------------------------------------------------------------------

using MediatR;

namespace SalaryInsights.Application.Commands.CreateSalary;

public record CreateSalaryCommand(
    Guid? CompanyId,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    decimal GrossAmount,
    decimal NetAmount,
    List<CreateSalaryItemDto> Items
) : IRequest<Guid>;