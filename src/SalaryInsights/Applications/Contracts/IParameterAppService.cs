// -----------------------------------------------------------------------
// <copyright file= "IParameterAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 11:6
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Dtos;
using SalaryInsights.Shared.Dtos;
using SalaryInsights.Shared.Enums;

namespace SalaryInsights.Applications.Contracts;

public interface IParameterAppService
{
    IList<SelectOptionDto> GetTypes();
    
    Task<IList<ParameterDto>> GetAsync(ParameterTypes parameterType, string name);

    Task<ParameterDto> GetByIdAsync(Guid id);

    Task<OperationResponseDto<Guid, ParameterDto>> CreateAsync(ParameterCreationDto creationDto);
    
    Task<OperationResponseDto<Guid, ParameterDto>> UpdateAsync(ParameterEditDto editDto);
    
    Task<OperationResponseDto<Guid, ParameterDto>> DeleteAsync(Guid id);
}