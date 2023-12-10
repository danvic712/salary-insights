// -----------------------------------------------------------------------
// <copyright file= "SelectOptionDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 11:26
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Shared.Enums;

namespace SalaryInsights.Dtos;

public class ParameterDto
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    public ParameterTypes ParameterType { get; set; }
    
    /// <summary>
    /// Parameter
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; }
}