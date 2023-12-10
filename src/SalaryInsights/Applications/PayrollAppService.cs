// -----------------------------------------------------------------------
// <copyright file= "PayrollAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 22:25
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SalaryInsights.Applications.Contracts;
using SalaryInsights.Dtos;
using SalaryInsights.EntityFrameworkCore;

namespace SalaryInsights.Applications;

public class PayrollAppService : IPayrollAppService
{
    #region Initializes

    private readonly SalaryInsightsDbContext _dbContext;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="dbContext"></param>
    public PayrollAppService(SalaryInsightsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Services

    public async Task<PayrollDetailsDto> GetByMonthAsync(DateTime month)
    {
        throw new NotImplementedException();
    }

    #endregion
}