// -----------------------------------------------------------------------
// <copyright file= "Parameter.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 16:19
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Enums;

namespace SalaryInsights.Models;

public class Parameter
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
    public string? Description { get; set; }
}