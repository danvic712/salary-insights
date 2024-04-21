// -----------------------------------------------------------------------
// <copyright file= "CompanyManager.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-4-5 22:24
// Modified by:
// Description:
// -----------------------------------------------------------------------

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SalaryInsights.Applications.Companies.Contracts;
using SalaryInsights.Applications.Companies.Dtos;
using SalaryInsights.Applications.Shared;
using SalaryInsights.Applications.Shared.Contracts;
using SalaryInsights.Applications.Shared.Dtos;
using SalaryInsights.Exceptions;
using SalaryInsights.Models;

namespace SalaryInsights.Applications.Companies;

public class CompanyManager : BaseManager, ICompanyManager
{
    #region Initialzies

    private readonly ILogger<CompanyManager> _logger;
    private readonly IMapper _mapper;
    private readonly SalaryInsightsDbContext _dbContext;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="currentUser">Current user instance</param>
    /// <param name="logger">Logger instance</param>
    /// <param name="mapper">AutoMapper instance</param>
    /// <param name="dbContext">DBContext instance</param>
    public CompanyManager(
        ICurrentUser currentUser,
        ILogger<CompanyManager> logger,
        IMapper mapper,
        SalaryInsightsDbContext dbContext) : base(currentUser)
    {
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    #endregion

    #region Services

    public async Task<IList<SelectOptionResponse>> GetCompaniesAsync()
    {
        var query = _dbContext.Companies.AsNoTracking()
            .Where(i => i.UserId == _currentUser.UserId)
            .Select(i => new SelectOptionResponse
            {
                Key = i.Id.ToString(),
                Value = i.Name,
                Label = i.Name
            })
            .OrderBy(i => i.Label);

        return await query.ToListAsync();
    }

    public async Task<GridResponse<CompanyResponse>> GetAsync(string name, DateTime? startDate, DateTime? endDate,
        GridRequest gridRequest)
    {
        var query = _dbContext.Companies.AsNoTracking()
            .Where(i => i.UserId == _currentUser.UserId);

        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim();
            query = query.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));
        }

        if (startDate.HasValue)
            query = query.Where(i => i.StartDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(i => i.EndDate <= endDate.Value);

        // Todo: Obtain user info.

        if (gridRequest.CurrentPage <= 1)
            gridRequest.CurrentPage = 1;

        var total = await query.CountAsync();
        var companies = await query
            .OrderByDescending(i => i.StartDate)
            .Skip((gridRequest.CurrentPage - 1) * gridRequest.PageSize)
            .Take(gridRequest.PageSize)
            .ProjectTo<CompanyResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new GridResponse<CompanyResponse>
        {
            CurrentPage = gridRequest.CurrentPage,
            TotalCount = total,
            Data = companies
        };
    }

    public async Task<CompanyResponse?> GetByIdAsync(Guid id)
    {
        var company = await _dbContext.Companies.AsNoTracking()
            .Where(i => i.Id == id)
            .ProjectTo<CompanyResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (company is null)
            return company;

        if (company.UserId != _currentUser.UserId)
            throw new BadHttpRequestException("Can not access this company.");

        return company;
    }

    public async Task<OperationResponse<Guid, CompanyResponse>> CreateAsync(CompanyCreationRequest creationRequest)
    {
        try
        {
            if (!creationRequest.CurrentlyEmployed)
                creationRequest.EndDate = null;

            var company = await _dbContext.Companies.AsNoTracking()
                .FirstOrDefaultAsync(i =>
                    i.UserId == _currentUser.UserId && i.Name == creationRequest.Name);
            if (company is null)
            {
                company = _mapper.Map<Company>(creationRequest);
                company.UserId = _currentUser.UserId;
                company.DateModified = DateTime.UtcNow;

                await _dbContext.Companies.AddAsync(company);
            }
            else
            {
                company.DateModified = DateTime.UtcNow;
            }

            await _dbContext.SaveChangesAsync();

            return OperationResponse<Guid, CompanyResponse>.Success(company.Id,
                _mapper.Map<CompanyResponse>(company));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new company,name:{Name}", creationRequest.Name);
            return OperationResponse<Guid, CompanyResponse>.Failure("Failed to create a new company.");
        }
    }

    public async Task<OperationResponse<Guid, CompanyResponse>> UpdateAsync(CompanyEditRequest editRequest)
    {
        try
        {
            var company = await _dbContext.Companies
                .FirstOrDefaultAsync(i => i.Id == editRequest.Id);
            if (company is null)
                throw new EntityNotFoundException("Company", editRequest.Id);

            if (company.UserId != _currentUser.UserId)
                throw new BadHttpRequestException("Can not access this company.");

            if (!editRequest.CurrentlyEmployed)
                editRequest.EndDate = null;

            _mapper.Map(editRequest, company);

            await _dbContext.SaveChangesAsync();

            return OperationResponse<Guid, CompanyResponse>.Success(company.Id,
                _mapper.Map<CompanyResponse>(company));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a company, id:{CompanyId}", editRequest.Id);
            return OperationResponse<Guid, CompanyResponse>.Failure("Failed to update a company.");
        }
    }

    public async Task<OperationResponse<Guid, CompanyResponse>> DeleteAsync(Guid id)
    {
        try
        {
            var company = await _dbContext.Companies
                .FirstOrDefaultAsync(i => i.Id == id);
            if (company is null)
                throw new EntityNotFoundException("Parameter", id);

            if (company.UserId != _currentUser.UserId)
                throw new BadHttpRequestException("Can not access this company.");

            var payrolls = await _dbContext.Payrolls.Where(i => i.CompanyId == id)
                .ToListAsync();
            if (payrolls.Any())
                payrolls.ForEach(i => i.CompanyId = null);

            _dbContext.Companies.Remove(company);

            await _dbContext.SaveChangesAsync();

            return OperationResponse<Guid, CompanyResponse>.Success(id, null);
        }
        catch (Exception ex) when (ex is not SIException)
        {
            _logger.LogError(ex, "An error occurred while deleting a company, id:{CompanyId}", id);
            return OperationResponse<Guid, CompanyResponse>.Failure("Failed to delete a company.");
        }
    }

    #endregion
}