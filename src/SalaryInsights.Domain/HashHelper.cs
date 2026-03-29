// -----------------------------------------------------------------------
// <copyright file= "HashHelper.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 17:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Domain;

public static class HashHelper
{
    public static async Task<string> ComputeHashAsync(
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = await sha.ComputeHashAsync(stream, cancellationToken);

        return Convert.ToHexStringLower(bytes);
    }
}