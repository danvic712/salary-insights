// -----------------------------------------------------------------------
// <copyright file= "IPayrollManager.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 22:23
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Applications.Payrolls.Dtos;
using SalaryInsights.Applications.Shared.Dtos;

namespace SalaryInsights.Applications.Payrolls.Contracts;

public interface IPayrollManager
{
    Task<GridResponse<PayrollDto>> QueryAsync(PayrollQueryDto queryDto);
    Task<PayrollDetailsDto?> GetByIdAsync(Guid id);
    Task<IList<SalaryItemDto>> GetSalaryItemsAsync(Guid id);
    Task<OperationResponse<Guid, PayrollDetailsDto>> CreateAsync(PayrollCreationDto creationDto);
    Task<OperationResponse<Guid, PayrollDetailsDto>> UpdateAsync(PayrollEditDto editDto);
    Task<OperationResponse<Guid, PayrollDetailsDto>> DeleteAsync(Guid id);
}