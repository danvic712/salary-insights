// -----------------------------------------------------------------------
// <copyright file= "ParametersController.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 11:4
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using SalaryInsights.Applications.Parameters.Contracts;
using SalaryInsights.Applications.Parameters.Dtos;
using SalaryInsights.Applications.Shared.Dtos;
using SalaryInsights.Enums;

namespace SalaryInsights.Controllers;

/// <summary>
/// Company resource endpoints
/// </summary>
[ApiController]
[Route("api/parameters")]
public class ParametersController : ControllerBase
{
    #region Initializes

    private readonly IParameterManager _manager;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="manager"></param>
    public ParametersController(IParameterManager manager)
    {
        _manager = manager;
    }

    #endregion

    #region APIs

    /// <summary>
    /// Returns the list of parameter types
    /// </summary>
    /// <returns></returns>
    [HttpGet("types")]
    public IList<SelectOptionResponse> GetTypes()
    {
        return _manager.GetTypes();
    }

    /// <summary>
    /// Return the list of parameters based on the provided type
    /// </summary>
    /// <param name="parameterType">Parameter type</param>
    /// <param name="name">Parameter name</param>
    /// <returns></returns>
    [HttpGet("types/{parameterType}")]
    public async Task<IList<ParameterDto>> GetAsync([FromRoute] ParameterTypes parameterType, [FromQuery] string? name)
    {
        return await _manager.GetAsync(parameterType, name);
    }

    /// <summary>
    /// Return the parameter details based on the provided id
    /// </summary>
    /// <param name="id">Parameter id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ParameterDto> GetByIdAsync([FromRoute] Guid id)
    {
        return await _manager.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new parameter
    /// </summary>
    /// <param name="creationRequest">Parameter creation dto</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<OperationResponse<Guid, ParameterDto>> CreateAsync([FromBody] ParameterCreationRequest creationRequest)
    {
        return await _manager.CreateAsync(creationRequest);
    }
    
    /// <summary>
    /// Update the existing parameter
    /// </summary>
    /// <param name="editDto">Parameter edit dto</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<OperationResponse<Guid, ParameterDto>> UpdateAsync([FromBody] ParameterEditDto editDto)
    {
        return await _manager.UpdateAsync(editDto);
    }
    
    /// <summary>
    /// Delete the existing parameter
    /// </summary>
    /// <param name="id">Parameter id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<OperationResponse<Guid, ParameterDto>> DeleteAsync([FromRoute] Guid id)
    {
        return await _manager.DeleteAsync(id);
    }

    #endregion
}