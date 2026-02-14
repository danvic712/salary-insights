// -----------------------------------------------------------------------
// <copyright file= "EFRepository.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SalaryInsights.Domain.Contracts;

namespace SalaryInsights.Infrastructure.Repositories;

public class EFRepository<TEntity>(DbContext dbContext) : IRepository<TEntity>
    where TEntity : class
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<TEntity>().Add(entity);
        return Task.CompletedTask;
    }
}