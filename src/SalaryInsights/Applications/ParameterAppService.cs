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
using SalaryInsights.Shared.Enums;
using SalaryInsights.Shared.Responses;

namespace SalaryInsights.Applications;

public class ParameterAppService : IParameterAppService
{
    #region Initialzies

    private readonly ILogger<ParameterAppService> _logger;
    private readonly IMapper _mapper;
    private readonly SalaryInsightsDbContext _dbContext;

    public ParameterAppService(ILogger<ParameterAppService> logger, IMapper mapper, SalaryInsightsDbContext dbContext)
    {
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    #endregion

    #region Services

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

    public async Task<OperationResponse<Guid, ParameterDto>> CreateAsync(ParameterCreationDto creationDto)
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

            return OperationResponse<Guid, ParameterDto>.Success(parameter.Id,
                _mapper.Map<ParameterDto>(parameter));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred while creating a new parameter, parameterType:{ParameterType}, name:{Name}",
                creationDto.ParameterType, creationDto.Name);

            return OperationResponse<Guid, ParameterDto>.Failure("Failed to create a new parameter.");
        }
    }

    #endregion
}