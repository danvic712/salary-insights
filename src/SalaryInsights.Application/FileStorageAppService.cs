// -----------------------------------------------------------------------
// <copyright file= "FileStorageAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 17:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.Agents.AI;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SalaryInsights.Application.Contracts;
using SalaryInsights.Domain;
using SalaryInsights.Domain.Contracts;
using SalaryInsights.Domain.Dtos;
using SalaryInsights.Domain.Dtos.FileStorage;
using SalaryInsights.Domain.Enums;
using SalaryInsights.Domain.Models;

namespace SalaryInsights.Application;

public class FileStorageAppService(
    ILogger<FileStorageAppService> logger,
    IUnitOfWork uow,
    [FromKeyedServices("salary-insights")] AIAgent agent) : IFileStorageAppService
{
    private const int BufferSize = 16 * 1024 * 1024; // 16 MB buffer size
    private const string UploadDirectory = "uploads";

    public async Task<FileHashDto> GetHashAsync(
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        var hash = await HashHelper.ComputeHashAsync(stream, cancellationToken);

        var fileExists = await uow.Get<FileAsset>()
            .GetQueryable(asNoTracking: true)
            .FirstOrDefaultAsync(f => f.FileHash == hash, cancellationToken);

        return new FileHashDto
        {
            Id = fileExists?.Id,
            FileHash = hash
        };
    }

    public async Task<OperationResult<FileAssetDto>> UploadAsync(
        string boundary,
        Stream contentStream,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), UploadDirectory);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var reader = new MultipartReader(boundary, contentStream);
            FileAsset? fileAsset = null;

            // Process each section in the multipart body
            while (await reader.ReadNextSectionAsync(cancellationToken) is { } section)
            {
                // Check if the section is a file
                var contentDisposition = section.GetContentDispositionHeader();
                if (contentDisposition == null || !contentDisposition.IsFileDisposition())
                    continue;

                var fileName = contentDisposition.FileName.Value ?? "unknown";
                var id = Guid.NewGuid();
                var targetFilePath = Path.Combine(
                    uploadPath,
                    id.ToString(),
                    $"{id}{Path.GetExtension(fileName)}");

                logger.LogInformation("Processing file: {FileName}, saving to: {TargetFilePath}", fileName,
                    targetFilePath);

                await using (var outputFileStream = new FileStream(
                                 path: targetFilePath,
                                 mode: FileMode.Create,
                                 access: FileAccess.Write,
                                 share: FileShare.None,
                                 bufferSize: BufferSize,
                                 useAsync: true))
                {
                    // Write the file content to the target file
                    await section.Body.CopyToAsync(outputFileStream, cancellationToken);
                }

                // Compute hash for the uploaded file
                await using var readStream = new FileStream(targetFilePath, FileMode.Open, FileAccess.Read,
                    FileShare.Read, BufferSize, useAsync: true);
                var hash = await HashHelper.ComputeHashAsync(readStream, cancellationToken);

                fileAsset = new FileAsset
                {
                    Id = id,
                    FileName = fileName,
                    FileSize = new FileInfo(targetFilePath).Length,
                    ContentType = section.ContentType ?? "application/octet-stream",
                    FileHash = hash,
                    StorageType = StorageTypes.Local,
                    FilePath = targetFilePath,
                    CreatedAt = DateTime.UtcNow
                };

                // Add to the database
                await uow.Get<FileAsset>().AddAsync(fileAsset, cancellationToken);
                await uow.CommitAsync(cancellationToken);

                // For now, we only process the first file in the multipart request if multiple files are sent
                break;
            }

            if (fileAsset == null)
            {
                return OperationResult<FileAssetDto>.Fail("No file found in the upload request");
            }

            return OperationResult<FileAssetDto>.Ok(new FileAssetDto
            {
                Id = fileAsset.Id,
                FileName = fileAsset.FileName,
                FileSize = fileAsset.FileSize,
                ContentType = fileAsset.ContentType,
                FileHash = fileAsset.FileHash,
                StorageType = fileAsset.StorageType,
                FilePath = fileAsset.FilePath,
                CreatedAt = fileAsset.CreatedAt
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during file upload");
            return OperationResult<FileAssetDto>.Fail("An error occurred during file upload");
        }
    }

    public async Task<IAsyncEnumerable<AgentResponseUpdate>?> AnalysisAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var fileAsset = await uow.Get<FileAsset>()
            .GetQueryable(true)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        if (fileAsset is null || !File.Exists(fileAsset.FilePath))
            return null;

        var message = $"Please help me analysis the payroll file of path {fileAsset.FilePath}";

        return agent.RunStreamingAsync(message: message, cancellationToken: cancellationToken);
    }
}