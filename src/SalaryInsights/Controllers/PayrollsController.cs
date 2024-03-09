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
using SalaryInsights.Applications.Payrolls.Contracts;
using SalaryInsights.Applications.Payrolls.Dtos;
using SalaryInsights.Applications.Shared.Dtos;

namespace SalaryInsights.Controllers;

[ApiController]
[Route("api/payrolls")]
public class PayrollsController : ControllerBase
{
    #region Initializes

    private readonly IPayrollManager _manager;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="manager"></param>
    public PayrollsController(IPayrollManager manager)
    {
        _manager = manager;
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
        return await _manager.QueryAsync(queryDto);
    }
    
    /// <summary>
    /// Return the payroll details based on the provided id
    /// </summary>
    /// <param name="id">Payroll id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<PayrollDetailsDto?> GetByIdAsync([FromRoute] Guid id)
    {
        return await _manager.GetByIdAsync(id);
    }
    
    /// <summary>
    /// Return the list of salary items based on the provided payroll id
    /// </summary>
    /// <param name="id">Payroll id</param>
    /// <returns></returns>
    [HttpGet("{id}/salary-items")]
    public async Task<IList<SalaryItemDto>> GetSalaryItemsAsync([FromRoute] Guid id)
    {
        return await _manager.GetSalaryItemsAsync(id);
    }
    
    /// <summary>
    /// Create a new payroll
    /// </summary>
    /// <param name="creationDto">Payroll creation dto</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<OperationResponse<Guid, PayrollDetailsDto>> CreateAsync([FromBody] PayrollCreationDto creationDto)
    {
        return await _manager.CreateAsync(creationDto);
    }
    
    /// <summary>
    /// Update the exist payroll
    /// </summary>
    /// <param name="editDto">Payroll edit dto</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<OperationResponse<Guid, PayrollDetailsDto>> UpdateAsync([FromBody] PayrollEditDto editDto)
    {
        return await _manager.UpdateAsync(editDto);
    }
    
    /// <summary>
    /// Delete the payroll record based on the provided id
    /// </summary>
    /// <param name="id">Payroll id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<OperationResponse<Guid, PayrollDetailsDto>> DeleteAsync([FromRoute] Guid id)
    {
        return await _manager.DeleteAsync(id);
    }

    #endregion
}