// -----------------------------------------------------------------------
// <copyright file= "SetupWizardStatusDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-24 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Application.Dtos.Setup;

public class SetupWizardStatusDto
{
    /// <summary>
    /// Total status: Is all configuration completed?
    /// </summary>
    public bool IsReady => AuthProviderConfigured && AIProviderConfigured;

    /// <summary>
    /// Currently displayed step for front-end Wizard UI
    /// 1: Auth configuration
    /// 2: AI configuration
    /// 3: Completed
    /// </summary>
    public int CurrentStep
    {
        get
        {
            if (!AuthProviderConfigured)
                return 1;

            return !AIProviderConfigured ? 2 : 3;
        }
    }

    /// <summary>
    /// Auth configuration status
    /// </summary>
    public bool AuthProviderConfigured { get; set; } = false;

    /// <summary>
    /// AI configuration status
    /// </summary>
    public bool AIProviderConfigured { get; set; } = false;
}