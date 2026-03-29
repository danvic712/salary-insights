// -----------------------------------------------------------------------
// <copyright file= "DataProtectorAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-24 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.DataProtection;
using SalaryInsights.Application.Contracts;

namespace SalaryInsights.Infrastructure;

public class DataProtectorAppService : IDataProtectorAppService
{
    private readonly IDataProtector _protector;

    /// <summary>
    /// C'tor
    /// </summary>
    /// <param name="provider">DataProtectionProvider injected by DI</param>
    /// <param name="purpose">Purpose string to isolate keys (optional)</param>
    public DataProtectorAppService(IDataProtectionProvider provider, string purpose = "SalaryInsightsDataProtector")
    {
        _protector = provider.CreateProtector(purpose);
    }

    public string Protect(string plainText) => _protector.Protect(plainText);

    public string Unprotect(string cipherText) => _protector.Unprotect(cipherText);
}