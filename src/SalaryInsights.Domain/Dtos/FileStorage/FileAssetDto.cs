// -----------------------------------------------------------------------
// <copyright file= "FileAssetDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 21:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Domain.Enums;

namespace SalaryInsights.Domain.Dtos.FileStorage;

public class FileAssetDto
{
    public Guid Id { get; set; }
    
    public string FileName { get; set; }

    public double FileSize { get; set; }

    public string ContentType { get; set; }
    
    public string FileHash { get; set; }

    public StorageTypes StorageType { get; set; } = StorageTypes.Local;

    public string FilePath { get; set; }

    public DateTime CreatedAt { get; set; }
}