// -----------------------------------------------------------------------
// <copyright file= "AuthProviderDataSeeder.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-27 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SalaryInsights.Domain.Contracts;
using SalaryInsights.Domain.Enums;
using SalaryInsights.Domain.Models;
using SalaryInsights.Infrastructure.Contracts;

namespace SalaryInsights.Infrastructure.EntityFrameworkCore.DataSeeders;

public class AuthProviderDataSeeder(IUnitOfWork unitOfWork) : IDataSeeder
{
    public long Order => 1;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var repo = unitOfWork.Get<AuthProvider>();

        var exists = await repo.GetQueryable()
            .Where(i => i.ProviderType == AuthProviderTypes.Supabase)
            .AnyAsync(cancellationToken);
        if (exists)
            return;

        var authProvider = new AuthProvider(AuthProviderTypes.Supabase);

        var authProviderSetting = new AuthProviderSetting(authProvider.Id, new
        {
            ProjectUrl = "https://hwkxrfyqkphtowdmbgqh.supabase.co",
            Jwt = new
            {
                Audience = "authenticated",
            }
        }, false);

        authProvider.SetProviderSetting(authProviderSetting);

        await repo.AddAsync(authProvider, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}