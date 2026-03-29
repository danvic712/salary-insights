// -----------------------------------------------------------------------
// <copyright file= "IFileStorageAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 17:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.Agents.AI;
using SalaryInsights.Domain.Dtos;
using SalaryInsights.Domain.Dtos.FileStorage;

namespace SalaryInsights.Application.Contracts;

public interface IFileStorageAppService
{
    Task<FileHashDto> GetHashAsync(
        Stream stream,
        CancellationToken cancellationToken = default);

    Task<OperationResult<FileAssetDto>> UploadAsync(
        string boundary,
        Stream contentStream,
        CancellationToken cancellationToken = default);

    Task<IAsyncEnumerable<AgentResponseUpdate>?> AnalysisAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}