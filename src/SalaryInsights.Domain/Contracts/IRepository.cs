// -----------------------------------------------------------------------
// <copyright file= "IRepository.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Domain.Contracts;

public interface IRepository<in TEntity>
    where TEntity : class
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}