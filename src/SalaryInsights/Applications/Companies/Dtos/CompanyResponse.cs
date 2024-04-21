// -----------------------------------------------------------------------
// <copyright file= "CompanyResponse.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-4-5 22:37
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.Text;
using System.Text.Json.Serialization;

namespace SalaryInsights.Applications.Companies.Dtos;

public class CompanyResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime? StartDate { get; set; }

    public virtual string StartDateStr => StartDate.HasValue
        ? StartDate.Value.ToString("yyyy-MM-dd")
        : "";

    public DateTime? EndDate { get; set; }
    public bool CurrentlyEmployed { get; set; }

    public virtual string EndDateStr => CurrentlyEmployed
        ? EndDate?.ToString("yyyy-MM-dd")
        : DateTime.UtcNow.AddDays(1 - DateTime.UtcNow.Day).ToString("yyyy-MM-dd");

    public bool FullTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Status
    {
        get
        {
            var status = new StringBuilder();
            status.Append(!CurrentlyEmployed ? "在职" : "离职");
            status.Append(FullTime ? "-全职" : "-兼职");
            return status.ToString();
        }
    }

    public Guid UserId { get; set; }

    public string UserName { get; set; }

    [JsonIgnore] public DateTime DateModified { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual string DateModifiedStr => DateModified.ToString("yyyy-MM-dd HH:mm:ss");
    
    /// <summary>
    /// 
    /// </summary>
    public string Remark { get; set; }
}