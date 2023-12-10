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
using SalaryInsights.Shared.Enums;
using SalaryInsights.Shared.Responses;

namespace SalaryInsights.Applications.Contracts;

public interface IParameterAppService
{
    Task<IList<ParameterDto>> GetAsync(ParameterTypes parameterType, string name);

    Task<ParameterDto> GetByIdAsync(Guid id);

    Task<OperationResponse<Guid, ParameterDto>> CreateAsync(ParameterCreationDto creationDto);
}