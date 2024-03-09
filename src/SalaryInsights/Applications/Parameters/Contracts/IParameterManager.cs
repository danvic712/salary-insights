// -----------------------------------------------------------------------
// <copyright file= "IParameterManager.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 11:6
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Applications.Parameters.Dtos;
using SalaryInsights.Applications.Shared.Dtos;
using SalaryInsights.Enums;

namespace SalaryInsights.Applications.Parameters.Contracts;

public interface IParameterManager
{
    IList<SelectOptionResponse> GetTypes();
    
    Task<IList<ParameterDto>> GetAsync(ParameterTypes parameterType, string name);

    Task<ParameterDto> GetByIdAsync(Guid id);

    Task<OperationResponse<Guid, ParameterDto>> CreateAsync(ParameterCreationRequest creationRequest);
    
    Task<OperationResponse<Guid, ParameterDto>> UpdateAsync(ParameterEditDto editDto);
    
    Task<OperationResponse<Guid, ParameterDto>> DeleteAsync(Guid id);
}