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
using SalaryInsights.Shared.Dtos;

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
    /// Returns the list of payrolls based on the provided conditions
    /// </summary>
    /// <param name="queryDto">Query conditions</param>
    /// <returns></returns>
    [HttpGet("query")]
    public async Task<PaginationResource<PayrollDto>> Query([FromQuery] PayrollQueryDto queryDto)
    {
        return await _appService.QueryAsync(queryDto);
    }
    
    /// <summary>
    /// Return the payroll details based on the provided id
    /// </summary>
    /// <param name="id">Payroll id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<PayrollDetailsDto?> GetByIdAsync([FromRoute] Guid id)
    {
        return await _appService.GetByIdAsync(id);
    }
    
    /// <summary>
    /// Return the list of salary items based on the provided payroll id
    /// </summary>
    /// <param name="id">Payroll id</param>
    /// <returns></returns>
    [HttpGet("{id}/salary-items")]
    public async Task<IList<SalaryItemDto>> GetSalaryItemsAsync([FromRoute] Guid id)
    {
        return await _appService.GetSalaryItemsAsync(id);
    }
    
    /// <summary>
    /// Create a new payroll
    /// </summary>
    /// <param name="creationDto">Payroll creation dto</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<OperationResponseDto<Guid, PayrollDetailsDto>> CreateAsync([FromBody] PayrollCreationDto creationDto)
    {
        return await _appService.CreateAsync(creationDto);
    }
    
    /// <summary>
    /// Update the exist payroll
    /// </summary>
    /// <param name="editDto">Payroll edit dto</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<OperationResponseDto<Guid, PayrollDetailsDto>> UpdateAsync([FromBody] PayrollEditDto editDto)
    {
        return await _appService.UpdateAsync(editDto);
    }
    
    /// <summary>
    /// Delete the payroll record based on the provided id
    /// </summary>
    /// <param name="id">Payroll id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<OperationResponseDto<Guid, PayrollDetailsDto>> DeleteAsync([FromRoute] Guid id)
    {
        return await _appService.DeleteAsync(id);
    }

    #endregion
}