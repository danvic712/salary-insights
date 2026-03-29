// -----------------------------------------------------------------------
// <copyright file= "EFRepository.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SalaryInsights.Domain.Contracts;

namespace SalaryInsights.Infrastructure.Repositories;

public class EFRepository<TEntity>(DbContext dbContext) : IRepository<TEntity>
    where TEntity : class
{
    public IQueryable<TEntity> GetQueryable(bool asNoTracking = false,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<TEntity>()
            .AddAsync(entity, cancellationToken);
    }
}