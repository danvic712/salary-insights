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
using SalaryInsights.Applications.Parameters.Dtos;
using SalaryInsights.Models;

namespace SalaryInsights;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ParameterCreationRequest, Parameter>(MemberList.Destination);
        
        CreateMap<Parameter, ParameterDto>(MemberList.Destination)
            .ReverseMap();
    }
}