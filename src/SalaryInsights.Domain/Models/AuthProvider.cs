// -----------------------------------------------------------------------
// <copyright file= "AuthProvider.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-27 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Domain.Enums;

namespace SalaryInsights.Domain.Models;

public class AuthProvider
{
    public Guid Id { get; private set; }

    public AuthProviderTypes ProviderType { get; private set; }

    public bool IsActive { get; private set; }

    public AuthProviderSetting ProviderSetting { get; private set; }

    private AuthProvider()
    {
    }

    public AuthProvider(AuthProviderTypes providerType, bool isActive = true)
    {
        Id = Guid.NewGuid();
        ProviderType = providerType;
        IsActive = isActive;
    }

    public void SetProviderSetting(AuthProviderSetting setting)
    {
        ProviderSetting = setting;
    }
}