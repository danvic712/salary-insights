// -----------------------------------------------------------------------
// <copyright file= "ParameterTypes.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 16:18
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace SalaryInsights.Shared.Enums;

public enum ParameterTypes : short
{
    [Display(Name = "Company")] Company = 1,

    [Display(Name = "Salary Item Type")] SalaryItemType,
    
    [Display(Name = "Currency")]Currency
}