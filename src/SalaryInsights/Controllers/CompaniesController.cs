// -----------------------------------------------------------------------
// <copyright file= "CompaniesController.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-4-5 22:18
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using SalaryInsights.Applications.Companies.Contracts;
using SalaryInsights.Applications.Companies.Dtos;
using SalaryInsights.Applications.Shared.Dtos;

namespace SalaryInsights.Controllers;

/// <summary>
/// Company resource endpoints
/// </summary>
[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    #region Initializes

    private readonly ICompanyManager _manager;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="manager"></param>
    public CompaniesController(ICompanyManager manager)
    {
        _manager = manager;
    }

    #endregion

    #region APIs

    /// <summary>
    /// Returns the list of companies
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    public async Task<IList<SelectOptionResponse>> GetCompaniesAsync()
    {
        return await _manager.GetCompaniesAsync();
    }

    /// <summary>
    /// Query companies based on the provided name
    /// </summary>
    /// <param name="name">Company name</param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="gridRequest">Standard grid request model</param>
    /// <returns></returns>
    [HttpGet("query")]
    public async Task<GridResponse<CompanyResponse>> GetAsync([FromQuery] string? name,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] GridRequest gridRequest)
    {
        return await _manager.GetAsync(name ?? "", startDate, endDate, gridRequest);
    }

    /// <summary>
    /// Return the company details based on the provided id
    /// </summary>
    /// <param name="id">Company id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<CompanyResponse?> GetByIdAsync([FromRoute] Guid id)
    {
        return await _manager.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new company
    /// </summary>
    /// <param name="creationRequest">Company creation dto</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<OperationResponse<Guid, CompanyResponse>> CreateAsync(
        [FromBody] CompanyCreationRequest creationRequest)
    {
        return await _manager.CreateAsync(creationRequest);
    }

    /// <summary>
    /// Update the existing company
    /// </summary>
    /// <param name="editDto">Company edit dto</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<OperationResponse<Guid, CompanyResponse>> UpdateAsync([FromBody] CompanyEditRequest editDto)
    {
        return await _manager.UpdateAsync(editDto);
    }

    /// <summary>
    /// Delete the existing company
    /// </summary>
    /// <param name="id">Company id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<OperationResponse<Guid, CompanyResponse>> DeleteAsync([FromRoute] Guid id)
    {
        return await _manager.DeleteAsync(id);
    }

    #endregion
}