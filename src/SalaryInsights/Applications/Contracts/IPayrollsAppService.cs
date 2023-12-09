// -----------------------------------------------------------------------
// <copyright file= "IPayrollsAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 22:23
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.ViewModels;

namespace SalaryInsights.Applications.Contracts;

public interface IPayrollsAppService
{
    Task<PayrollDetailsVM> GetByMonthAsync(DateTime month);
}