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
using SalaryInsights.Applications.Contracts;
using SalaryInsights.Dtos;
using SalaryInsights.Shared.Dtos;
using SalaryInsights.Shared.Enums;

namespace SalaryInsights.Controllers;

/// <summary>
/// Company resource endpoints
/// </summary>
[ApiController]
[Route("api/parameters")]
public class ParametersController : ControllerBase
{
    #region Initializes

    private readonly IParameterAppService _appService;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="appService"></param>
    public ParametersController(IParameterAppService appService)
    {
        _appService = appService;
    }

    #endregion

    #region APIs

    /// <summary>
    /// Returns the list of parameter types
    /// </summary>
    /// <returns></returns>
    [HttpGet("types")]
    public IList<SelectOptionDto> GetTypes()
    {
        return _appService.GetTypes();
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
        return await _appService.GetAsync(parameterType, name);
    }

    /// <summary>
    /// Return the parameter details based on the provided id
    /// </summary>
    /// <param name="id">Parameter id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ParameterDto> GetByIdAsync([FromRoute] Guid id)
    {
        return await _appService.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new parameter
    /// </summary>
    /// <param name="creationDto">Parameter creation dto</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<OperationResponseDto<Guid, ParameterDto>> CreateAsync([FromBody] ParameterCreationDto creationDto)
    {
        return await _appService.CreateAsync(creationDto);
    }
    
    /// <summary>
    /// Update the existing parameter
    /// </summary>
    /// <param name="editDto">Parameter edit dto</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<OperationResponseDto<Guid, ParameterDto>> UpdateAsync([FromBody] ParameterEditDto editDto)
    {
        return await _appService.UpdateAsync(editDto);
    }
    
    /// <summary>
    /// Delete the existing parameter
    /// </summary>
    /// <param name="id">Parameter id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<OperationResponseDto<Guid, ParameterDto>> DeleteAsync([FromRoute] Guid id)
    {
        return await _appService.DeleteAsync(id);
    }

    #endregion
}