// -----------------------------------------------------------------------
// <copyright file= "ParameterCreationRequest.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 16:48
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using SalaryInsights.Enums;

namespace SalaryInsights.Applications.Parameters.Dtos;

public class ParameterCreationRequest
{
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