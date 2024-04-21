// -----------------------------------------------------------------------
// <copyright file= "CompanyCreationRequest.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-4-5 22:43
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace SalaryInsights.Applications.Companies.Dtos;

public class CompanyCreationRequest
{
    [Required] [MaxLength(50)] public string Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public bool CurrentlyEmployed { get; set; }

    public bool FullTime { get; set; } = true;
    
    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }
}