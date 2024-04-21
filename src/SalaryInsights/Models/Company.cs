// -----------------------------------------------------------------------
// <copyright file= "Company.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-4-5 21:58
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Models;

public class Company
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool CurrentlyEmployed { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool FullTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime DateModified { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }
}