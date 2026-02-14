// -----------------------------------------------------------------------
// <copyright file= "CreateSalaryCommandHandler.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-12 22:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using MediatR;
using Microsoft.Extensions.Logging;
using SalaryInsights.Domain.Contracts;
using SalaryInsights.Domain.Models;

namespace SalaryInsights.Application.Commands.CreateSalary;

public class CreateSalaryCommandHandler(ILogger<CreateSalaryCommandHandler> logger, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateSalaryCommand, Guid>
{
    public async Task<Guid> Handle(CreateSalaryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var salary = new Salary(
                command.CompanyId,
                command.PeriodStart,
                command.PeriodEnd,
                command.GrossAmount,
                command.NetAmount);

            foreach (var item in command.Items)
            {
                salary.AddItem(item.Name, item.Type, item.Amount);
            }

            await unitOfWork.GetRepository<Salary>()
                .AddAsync(salary, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);

            return salary.Id;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating salary");
            throw;
        }
    }
}