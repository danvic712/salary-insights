// -----------------------------------------------------------------------
// <copyright file= "ParameterAppService.cs">
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
using SalaryInsights.Applications.Contracts;
using SalaryInsights.Dtos;
using SalaryInsights.EntityFrameworkCore;
using SalaryInsights.EntityFrameworkCore.Models;
using SalaryInsights.Shared;
using SalaryInsights.Shared.Dtos;
using SalaryInsights.Shared.Enums;
using SalaryInsights.Shared.Exceptions;

namespace SalaryInsights.Applications;

public class ParameterAppService : IParameterAppService
{
    #region Initialzies

    private readonly ILogger<ParameterAppService> _logger;
    private readonly IMapper _mapper;
    private readonly SalaryInsightsDbContext _dbContext;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="mapper">AutoMapper instance</param>
    /// <param name="dbContext">DBContext instance</param>
    public ParameterAppService(
        ILogger<ParameterAppService> logger,
        IMapper mapper,
        SalaryInsightsDbContext dbContext)
    {
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    #endregion

    #region Services

    public IList<SelectOptionDto> GetTypes()
    {
        return Utils.GetEnumOptions<ParameterTypes>()
            .OrderBy(i => i.Text)
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

    public async Task<OperationResponseDto<Guid, ParameterDto>> CreateAsync(ParameterCreationDto creationDto)
    {
        try
        {
            var parameter = await _dbContext.Parameters.AsNoTracking()
                .FirstOrDefaultAsync(i => i.ParameterType == creationDto.ParameterType && i.Name == creationDto.Name);
            if (parameter is null)
            {
                parameter = _mapper.Map<Parameter>(creationDto);

                await _dbContext.Parameters.AddAsync(parameter);

                await _dbContext.SaveChangesAsync();
            }

            return OperationResponseDto<Guid, ParameterDto>.Success(parameter.Id,
                _mapper.Map<ParameterDto>(parameter));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred while creating a new parameter, parameterType:{ParameterType}, name:{Name}",
                creationDto.ParameterType, creationDto.Name);

            return OperationResponseDto<Guid, ParameterDto>.Failure("Failed to create a new parameter.");
        }
    }

    public async Task<OperationResponseDto<Guid, ParameterDto>> UpdateAsync(ParameterEditDto editDto)
    {
        try
        {
            var parameter = await _dbContext.Parameters
                .FirstOrDefaultAsync(i => i.Id == editDto.Id);
            if (parameter is null)
                throw new EntityNotFoundException();

            _mapper.Map(editDto, parameter);

            return OperationResponseDto<Guid, ParameterDto>.Success(parameter.Id,
                _mapper.Map<ParameterDto>(parameter));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating a parameter, id:{ParameterId}", editDto.Id);
            return OperationResponseDto<Guid, ParameterDto>.Failure("Failed to update a parameter.");
        }
    }

    public async Task<OperationResponseDto<Guid, ParameterDto>> DeleteAsync(Guid id)
    {
        try
        {
            var parameter = await _dbContext.Parameters
                .FirstOrDefaultAsync(i => i.Id == id);
            if (parameter is null)
                throw new EntityNotFoundException();

            _dbContext.Parameters.Remove(parameter);

            await _dbContext.SaveChangesAsync();

            return OperationResponseDto<Guid, ParameterDto>.Success(id, null);
        }
        catch (Exception ex) when (ex is not SIException)
        {
            _logger.LogError(ex, "An error occurred while deleting a parameter, id:{ParameterId}", id);
            return OperationResponseDto<Guid, ParameterDto>.Failure("Failed to delete a parameter.");
        }
    }

    #endregion
}