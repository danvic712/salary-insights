// -----------------------------------------------------------------------
// <copyright file= "FileHashDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 20:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Domain.Dtos.FileStorage;

public class FileHashDto
{
    public Guid? Id { get; set; }

    public string FileHash { get; set; }
}