// -----------------------------------------------------------------------
// <copyright file= "Utils.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-2-4 21:51
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using SalaryInsights.Applications.Shared.Dtos;

namespace SalaryInsights;

public static class Utils
{
    public static List<SelectOptionResponse> GetEnumOptions<T>() where T : Enum
    {
        var options = new List<SelectOptionResponse>();

        foreach (T value in Enum.GetValues(typeof(T)))
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                continue;

            var text = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is not DisplayAttribute attribute
                ? value.ToString()
                : attribute.Name;

            options.Add(new SelectOptionResponse
            {
                Key = Convert.ToInt32(value).ToString(),
                Value = value.ToString(),
                Label = text ?? ""
            });
        }

        return options;
    }
}