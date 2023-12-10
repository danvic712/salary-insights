// -----------------------------------------------------------------------
// <copyright file= "MapperProfile.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 11:33
// Modified by:
// Description: AutoMapper configurations
// -----------------------------------------------------------------------

using AutoMapper;
using SalaryInsights.Dtos;
using SalaryInsights.EntityFrameworkCore.Models;

namespace SalaryInsights;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ParameterCreationDto, Parameter>(MemberList.Destination);
        
        CreateMap<Parameter, ParameterDto>(MemberList.Destination)
            .ReverseMap();
    }
}