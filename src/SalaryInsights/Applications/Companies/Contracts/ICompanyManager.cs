// -----------------------------------------------------------------------
// <copyright file= "ICompanyManager.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-4-5 22:24
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Applications.Companies.Dtos;
using SalaryInsights.Applications.Shared.Dtos;

namespace SalaryInsights.Applications.Companies.Contracts;

public interface ICompanyManager
{
    Task<IList<SelectOptionResponse>> GetCompaniesAsync();

    Task<GridResponse<CompanyResponse>> GetAsync(string name, DateTime? startDate, DateTime? endDate,
        GridRequest gridRequest);

    Task<CompanyResponse?> GetByIdAsync(Guid id);

    Task<OperationResponse<Guid, CompanyResponse>> CreateAsync(CompanyCreationRequest creationRequest);

    Task<OperationResponse<Guid, CompanyResponse>> UpdateAsync(CompanyEditRequest editRequest);

    Task<OperationResponse<Guid, CompanyResponse>> DeleteAsync(Guid id);
}