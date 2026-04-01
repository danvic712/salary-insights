// -----------------------------------------------------------------------
// <copyright file= "AIProviderTypes.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-04-01 21:04
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Domain.Enums;

public enum AIProviderTypes : short
{
    OpenAI = 1,

    AzureOpenAI,

    Anthropic,

    Ollama,

    Gemini,

    OpenAICompatible,
}