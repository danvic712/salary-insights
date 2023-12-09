// -----------------------------------------------------------------------
// <copyright file= "PayrollsAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 22:25
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SalaryInsights.Applications.Contracts;
using SalaryInsights.ViewModels;

namespace SalaryInsights.Applications;

public class PayrollsAppService : IPayrollsAppService
{
    #region Initializes

    private readonly DbContext _dbContext;

    /// <summary>
    /// C'tor
    /// </summary>
    /// <param name="dbContext"></param>
    public PayrollsAppService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Services

    public async Task<PayrollDetailsVM> GetByMonthAsync(DateTime month)
    {
        throw new NotImplementedException();
    }

    #endregion
}