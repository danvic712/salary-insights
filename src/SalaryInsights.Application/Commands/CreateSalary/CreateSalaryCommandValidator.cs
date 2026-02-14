// -----------------------------------------------------------------------
// <copyright file= "CreateSalaryCommandValidator.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 23:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using FluentValidation;

namespace SalaryInsights.Application.Commands.CreateSalary;

public class CreateSalaryCommandValidator : AbstractValidator<CreateSalaryCommand>
{
    public CreateSalaryCommandValidator()
    {
    }
}