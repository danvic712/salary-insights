// -----------------------------------------------------------------------
// <copyright file= "PayrollDetailsDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-9 22:27
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Dtos;

public class PayrollDetailsDto : PayrollDto
{
    /// <summary>
    /// 
    /// </summary>
    public IList<SalaryItemDto> SalaryItems { get; set; } = new List<SalaryItemDto>();
}