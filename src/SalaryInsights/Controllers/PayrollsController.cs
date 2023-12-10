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
using SalaryInsights.Dtos;

namespace SalaryInsights.Controllers;

[ApiController]
[Route("api/payrolls")]
public class PayrollsController : ControllerBase
{
    #region Initializes

    private readonly IPayrollAppService _appService;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="appService"></param>
    public PayrollsController(IPayrollAppService appService)
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
    public async Task<PayrollDetailsDto> GetByMonth([FromRoute] DateTime month)
    {
        return await _appService.GetByMonthAsync(month);
    }

    #endregion
}