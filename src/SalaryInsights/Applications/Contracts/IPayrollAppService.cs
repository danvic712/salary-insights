// -----------------------------------------------------------------------
// <copyright file= "IPayrollAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 22:23
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Dtos;
using SalaryInsights.Shared.Dtos;

namespace SalaryInsights.Applications.Contracts;

public interface IPayrollAppService
{
    Task<PaginationResource<PayrollDto>> QueryAsync(PayrollQueryDto queryDto);
    Task<PayrollDetailsDto?> GetByIdAsync(Guid id);
    Task<IList<SalaryItemDto>> GetSalaryItemsAsync(Guid id);
    Task<OperationResponseDto<Guid, PayrollDetailsDto>> CreateAsync(PayrollCreationDto creationDto);
    Task<OperationResponseDto<Guid, PayrollDetailsDto>> UpdateAsync(PayrollEditDto editDto);
    Task<OperationResponseDto<Guid, PayrollDetailsDto>> DeleteAsync(Guid id);
}