// -----------------------------------------------------------------------
// <copyright file= "AIModel.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-04-01 21:20
// Modified by:
// Description: AI Model Entity
// -----------------------------------------------------------------------

namespace SalaryInsights.Domain.Models;

public class AIModel
{
    public Guid Id { get; set; }

    public Guid AIProviderId { get; set; }

    public string DeploymentName { get; set; }

    public string Description { get; set; }

    public string ExtraInfo { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AIProvider AIProvider { get; set; }
}