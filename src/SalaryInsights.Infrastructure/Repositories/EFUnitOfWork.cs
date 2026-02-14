// -----------------------------------------------------------------------
// <copyright file= "EFUnitOfWork.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using SalaryInsights.Domain.Contracts;

namespace SalaryInsights.Infrastructure.Repositories;

public class EFUnitOfWork(SalaryInsightsDbContext dbContext)
    : IUnitOfWork
{
    private readonly ConcurrentDictionary<Type, Lazy<object>> _repositories = new();
    
    public IRepository<TEntity> GetRepository<TEntity>() 
        where TEntity : class
    {
        var type = typeof(TEntity);

        var lazy = _repositories.GetOrAdd(
            type,
            _ => new Lazy<object>(() => new EFRepository<TEntity>(dbContext), isThreadSafe: true));

        return (IRepository<TEntity>)lazy.Value;
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);
}