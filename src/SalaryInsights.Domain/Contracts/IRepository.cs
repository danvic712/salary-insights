// -----------------------------------------------------------------------
// <copyright file= "IRepository.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.Linq.Expressions;

namespace SalaryInsights.Domain.Contracts;

public interface IRepository<TEntity>
    where TEntity : class
{
    IQueryable<TEntity> GetQueryable(bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}