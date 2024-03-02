// -----------------------------------------------------------------------
// <copyright file= "PayrollAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 22:25
// Modified by:
// Description:
// -----------------------------------------------------------------------

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SalaryInsights.Applications.Contracts;
using SalaryInsights.Dtos;
using SalaryInsights.EntityFrameworkCore;
using SalaryInsights.EntityFrameworkCore.Models;
using SalaryInsights.Shared.Dtos;
using SalaryInsights.Shared.Enums;
using SalaryInsights.Shared.Exceptions;

namespace SalaryInsights.Applications;

public class PayrollAppService : IPayrollAppService
{
    #region Initializes

    private readonly ILogger<PayrollAppService> _logger;
    private readonly IMapper _mapper;
    private readonly SalaryInsightsDbContext _dbContext;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    /// <param name="dbContext"></param>
    public PayrollAppService(
        ILogger<PayrollAppService> logger,
        IMapper mapper,
        SalaryInsightsDbContext dbContext)
    {
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    #endregion

    #region Services

    public async Task<PaginationResource<PayrollDto>> QueryAsync(PayrollQueryDto queryDto)
    {
        var query = GetPayrolls();

        if (queryDto.StartTime.HasValue)
            query = query.Where(p => p.Month >= queryDto.StartTime.Value);

        if (queryDto.EndTime.HasValue)
            query = query.Where(p => p.Month <= queryDto.EndTime.Value);

        if (!string.IsNullOrWhiteSpace(queryDto.CompanyName))
        {
            var companyName = queryDto.CompanyName.Trim();
            query = query.Where(i => i.CompanyName.Contains(companyName));
        }

        var total = await query.CountAsync();
        var payrolls = await query.Skip((queryDto.Page - 1) * queryDto.PageSize)
            .Take(queryDto.PageSize)
            .ToListAsync();

        return new PaginationResource<PayrollDto>
        {
            TotalCount = total,
            Data = payrolls
        };
    }

    public async Task<PayrollDetailsDto?> GetByIdAsync(Guid id)
    {
        var payroll = await GetPayrolls().ProjectTo<PayrollDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(i => i.Id == id);
        if (payroll is null)
            return null;

        payroll.SalaryItems = await GetSalaryItems(id).ToListAsync();

        return payroll;
    }

    public async Task<IList<SalaryItemDto>> GetSalaryItemsAsync(Guid id)
    {
        return await GetSalaryItems(id).ToListAsync();
    }

    public async Task<OperationResponseDto<Guid, PayrollDetailsDto>> CreateAsync(PayrollCreationDto creationDto)
    {
        try
        {
            var companyParameter = await _dbContext.Parameters
                .FirstOrDefaultAsync(i => i.ParameterType == ParameterTypes.Company && i.Id == creationDto.CompanyId);
            if (companyParameter is null)
                throw new EntityNotFoundException("Company", creationDto.CompanyId);

            var payroll = _mapper.Map<Payroll>(creationDto);

            await _dbContext.Payrolls.AddAsync(payroll);

            await SalaryItemHandlerAsync(payroll.Id, creationDto.SalaryItems);

            await _dbContext.SaveChangesAsync();

            return OperationResponseDto<Guid, PayrollDetailsDto>.Success(payroll.Id,
                await GetByIdAsync(payroll.Id));
        }
        catch (Exception ex) when (ex is not SIException)
        {
            _logger.LogError(ex, "An error occurred while creating the payroll");
            return OperationResponseDto<Guid, PayrollDetailsDto>.Failure("Failed to create the payroll.");
        }
    }

    public async Task<OperationResponseDto<Guid, PayrollDetailsDto>> UpdateAsync(PayrollEditDto editDto)
    {
        try
        {
            var payroll = await _dbContext.Payrolls.FirstOrDefaultAsync(i => i.Id == editDto.Id);
            if (payroll is null)
                throw new EntityNotFoundException("Payroll", editDto.Id);

            var salaryItems = await _dbContext.SalaryItems.Where(i => i.PayrollId == editDto.Id)
                .ToListAsync();
            if (salaryItems.Count != 0)
                _dbContext.SalaryItems.RemoveRange(salaryItems);

            await SalaryItemHandlerAsync(payroll.Id, editDto.SalaryItems);

            await _dbContext.SaveChangesAsync();

            return OperationResponseDto<Guid, PayrollDetailsDto>.Success(payroll.Id,
                await GetByIdAsync(payroll.Id));
        }
        catch (Exception ex) when (ex is not SIException)
        {
            _logger.LogError(ex, "An error occurred while updating the payroll:{Id}", editDto.Id);
            return OperationResponseDto<Guid, PayrollDetailsDto>.Failure("Failed to update the payroll.");
        }
    }

    public async Task<OperationResponseDto<Guid, PayrollDetailsDto>> DeleteAsync(Guid id)
    {
        try
        {
            var payroll = await _dbContext.Payrolls.FirstOrDefaultAsync(i => i.Id == id);
            if (payroll is null)
                throw new EntityNotFoundException("Payroll", id);

            var salaryItems = await _dbContext.SalaryItems.Where(i => i.PayrollId == id)
                .ToListAsync();
            if (salaryItems.Count != 0)
                _dbContext.SalaryItems.RemoveRange(salaryItems);

            _dbContext.Payrolls.Remove(payroll);

            await _dbContext.SaveChangesAsync();

            return OperationResponseDto<Guid, PayrollDetailsDto>.Success(id,
                null);
        }
        catch (Exception ex) when (ex is not SIException)
        {
            _logger.LogError(ex, "An error occurred while deleting the payroll:{Id}", id);
            return OperationResponseDto<Guid, PayrollDetailsDto>.Failure("Failed to delete the payroll.");
        }
    }

    #endregion

    #region Methods

    private IQueryable<PayrollDto> GetPayrolls()
    {
        var query = from p in _dbContext.Payrolls.AsNoTracking()
            join c in _dbContext.Parameters.AsNoTracking() on p.CompanyId equals c.Id
            where c.ParameterType == ParameterTypes.Company
            orderby p.Month descending
            select new PayrollDto
            {
                Id = p.Id,
                Month = p.Month,
                CompanyId = p.CompanyId,
                CompanyName = c.Name,
                NetSalary = p.NetSalary,
                GrossSalary = p.GrossSalary,
                Remark = p.Remark
            };

        return query;
    }

    private IQueryable<SalaryItemDto> GetSalaryItems(Guid payrollId)
    {
        var salaryItems = from s in _dbContext.SalaryItems.AsNoTracking()
            join si in _dbContext.Parameters.AsNoTracking() on s.SalaryItemTypeId equals si.Id
            where s.PayrollId == payrollId && si.ParameterType == ParameterTypes.SalaryItemType
            orderby si.Name ascending
            select new SalaryItemDto
            {
                Id = s.Id,
                SalaryItemTypeId = s.SalaryItemTypeId,
                SalaryItemType = si.Name,
                Amount = s.Amount,
                Income = s.Income
            };

        return salaryItems;
    }

    private async Task SalaryItemHandlerAsync(Guid payrollId, IList<SalaryItemCreationDto> salaryItems)
    {
        if (payrollId == Guid.Empty || !salaryItems.Any())
            return;

        var salaryItemTypeIds = salaryItems.Select(i => i.SalaryItemTypeId)
            .Distinct()
            .ToList();

        var salaryItemParameterIds = await _dbContext.Parameters
            .Where(i => i.ParameterType == ParameterTypes.SalaryItemType && salaryItemTypeIds.Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync();

        var invalidIds = salaryItemTypeIds.Except(salaryItemParameterIds);
        if (invalidIds.Any())
            throw new EntityNotFoundException("Salary Item Types", string.Join(',', invalidIds));

        var items = _mapper.Map<List<SalaryItem>>(salaryItems);

        items.ForEach(i => i.PayrollId = payrollId);
        await _dbContext.SalaryItems.AddRangeAsync(items);
    }

    #endregion
}