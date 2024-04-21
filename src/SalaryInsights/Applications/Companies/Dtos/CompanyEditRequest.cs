// -----------------------------------------------------------------------
// <copyright file= "CompanyEditRequest.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-4-5 22:44
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace SalaryInsights.Applications.Companies.Dtos;

public class CompanyEditRequest
{
    [Required] public Guid Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public bool CurrentlyEmployed { get; set; }
    
    public bool FullTime { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }
}