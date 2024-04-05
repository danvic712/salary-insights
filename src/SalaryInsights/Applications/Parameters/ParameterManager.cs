// -----------------------------------------------------------------------
// <copyright file= "ParameterManager.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 11:10
// Modified by:
// Description:
// -----------------------------------------------------------------------

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SalaryInsights.Applications.Parameters.Contracts;
using SalaryInsights.Applications.Parameters.Dtos;
using SalaryInsights.Applications.Shared;
using SalaryInsights.Applications.Shared.Contracts;
using SalaryInsights.Applications.Shared.Dtos;
using SalaryInsights.Enums;
using SalaryInsights.Exceptions;
using SalaryInsights.Models;

namespace SalaryInsights.Applications.Parameters;

public class ParameterManager : BaseManager, IParameterManager
{
    #region Initialzies

    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ParameterManager> _logger;
    private readonly IMapper _mapper;
    private readonly SalaryInsightsDbContext _dbContext;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="currentUser">Current user instance</param>
    /// <param name="logger">Logger instance</param>
    /// <param name="mapper">AutoMapper instance</param>
    /// <param name="dbContext">DBContext instance</param>
    public ParameterManager(
        ICurrentUser currentUser,
        ILogger<ParameterManager> logger,
        IMapper mapper,
        SalaryInsightsDbContext dbContext) : base(currentUser)
    {
        _currentUser = currentUser;
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    #endregion

    #region Services

    public IList<SelectOptionResponse> GetTypes()
    {
        return Utils.GetEnumOptions<ParameterTypes>()
            .OrderBy(i => i.Label)
            .ToList();
    }

    public async Task<IList<ParameterDto>> GetAsync(ParameterTypes parameterType, string name)
    {
        var query = _dbContext.Parameters.AsNoTracking()
            .Where(i => i.ParameterType == parameterType);

        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim();
            query = query.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));
        }

        return await query
            .OrderBy(i => i.Name)
            .ProjectTo<ParameterDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ParameterDto> GetByIdAsync(Guid id)
    {
        return await _dbContext.Parameters.AsNoTracking()
            .Where(i => i.Id == id)
            .ProjectTo<ParameterDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<OperationResponse<Guid, ParameterDto>> CreateAsync(ParameterCreationRequest creationRequest)
    {
        try
        {
            var parameter = await _dbContext.Parameters.AsNoTracking()
                .FirstOrDefaultAsync(i =>
                    i.ParameterType == creationRequest.ParameterType && i.Name == creationRequest.Name);
            if (parameter is null)
            {
                parameter = _mapper.Map<Parameter>(creationRequest);

                await _dbContext.Parameters.AddAsync(parameter);

                await _dbContext.SaveChangesAsync();
            }

            return OperationResponse<Guid, ParameterDto>.Success(parameter.Id,
                _mapper.Map<ParameterDto>(parameter));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred while creating a new parameter, parameterType:{ParameterType}, name:{Name}",
                creationRequest.ParameterType, creationRequest.Name);

            return OperationResponse<Guid, ParameterDto>.Failure("Failed to create a new parameter.");
        }
    }

    public async Task<OperationResponse<Guid, ParameterDto>> UpdateAsync(ParameterEditDto editDto)
    {
        try
        {
            var parameter = await _dbContext.Parameters
                .FirstOrDefaultAsync(i => i.Id == editDto.Id);
            if (parameter is null)
                throw new EntityNotFoundException("Parameter", editDto.Id);

            _mapper.Map(editDto, parameter);

            return OperationResponse<Guid, ParameterDto>.Success(parameter.Id,
                _mapper.Map<ParameterDto>(parameter));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a parameter, id:{ParameterId}", editDto.Id);
            return OperationResponse<Guid, ParameterDto>.Failure("Failed to update a parameter.");
        }
    }

    public async Task<OperationResponse<Guid, ParameterDto>> DeleteAsync(Guid id)
    {
        try
        {
            var parameter = await _dbContext.Parameters
                .FirstOrDefaultAsync(i => i.Id == id);
            if (parameter is null)
                throw new EntityNotFoundException("Parameter", id);

            _dbContext.Parameters.Remove(parameter);

            await _dbContext.SaveChangesAsync();

            return OperationResponse<Guid, ParameterDto>.Success(id, null);
        }
        catch (Exception ex) when (ex is not SIException)
        {
            _logger.LogError(ex, "An error occurred while deleting a parameter, id:{ParameterId}", id);
            return OperationResponse<Guid, ParameterDto>.Failure("Failed to delete a parameter.");
        }
    }

    #endregion
}