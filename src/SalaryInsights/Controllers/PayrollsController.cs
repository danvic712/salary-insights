// -----------------------------------------------------------------------
// <copyright file= "PayrollsController.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 20:25
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using SalaryInsights.Applications.Contracts;
using SalaryInsights.ViewModels;

namespace SalaryInsights.Controllers;

[ApiController]
[Route("api/payrolls")]
public class PayrollsController : ControllerBase
{
    #region Initializes

    private readonly IPayrollsAppService _appService;

    /// <summary>
    /// C'tor
    /// </summary>
    /// <param name="appService"></param>
    public PayrollsController(IPayrollsAppService appService)
    {
        _appService = appService;
    }

    #endregion

    #region APIs

    /// <summary>
    /// 
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    [HttpGet("{month}")]
    public async Task<PayrollDetailsVM> GetByMonth([FromRoute] DateTime month)
    {
        return await _appService.GetByMonthAsync(month);
    }

    #endregion
}