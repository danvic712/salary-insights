// -----------------------------------------------------------------------
// <copyright file= "FileAsset.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 17:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Domain.Enums;

namespace SalaryInsights.Domain.Models;

public class FileAsset
{
    public Guid Id { get; set; }

    public string FileName { get; set; }

    public double FileSize { get; set; }

    public string ContentType { get; set; }
    
    public string FileHash { get; set; }

    public StorageTypes StorageType { get; set; }

    public string FilePath { get; set; }

    public DateTime CreatedAt { get; set; }
}