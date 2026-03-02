// -----------------------------------------------------------------------
// <copyright file= "AuthProviderSetting.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-27 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.Text.Json;

namespace SalaryInsights.Domain.Models;

public class AuthProviderSetting
{
    public Guid Id { get; private set; }

    public Guid AuthProviderId { get; private set; }

    public string Setting { get; private set; } = null!;

    public AuthProvider AuthProvider { get; private set; } = null!;

    public bool IsInitialized { get; set; }

    private AuthProviderSetting()
    {
    }

    public AuthProviderSetting(Guid authProviderId, object configuration, bool isInitialized)
    {
        Id = Guid.NewGuid();
        AuthProviderId = authProviderId;
        Setting = JsonSerializer.Serialize(configuration);
        IsInitialized = isInitialized;
    }

    public T GetSettings<T>() => JsonSerializer.Deserialize<T>(Setting)!;
}