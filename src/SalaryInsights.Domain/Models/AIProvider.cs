// -----------------------------------------------------------------------
// <copyright file= "AIProvider.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-04-01 21:20
// Modified by:
// Description: AI Provider Entity
// -----------------------------------------------------------------------

using SalaryInsights.Domain.Enums;

namespace SalaryInsights.Domain.Models;

public class AIProvider
{
    public Guid Id { get; set; }

    public AIProviderTypes AIProviderType { get; set; }

    public string Description { get; set; }

    public string Endpoint { get; set; }

    public string APIKey { get; set; }

    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual HashSet<AIModel> Models { get; set; } = [];
}