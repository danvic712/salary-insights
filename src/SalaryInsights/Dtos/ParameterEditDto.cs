// -----------------------------------------------------------------------
// <copyright file= "ParameterEditDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-2-4 21:24
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using SalaryInsights.Shared.Enums;

namespace SalaryInsights.Dtos;

public class ParameterEditDto
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Type
    /// </summary>
    [Required]
    public ParameterTypes ParameterType { get; set; }
    
    /// <summary>
    /// Parameter
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
}