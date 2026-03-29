// -----------------------------------------------------------------------
// <copyright file= "ConfigureSwaggerOptions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 16:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using SalaryInsights.Application;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SalaryInsights.Extensions;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<ApiVersioningOptions> options)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        // 1. Include XML comments from API and Application projects
        //
        var assemblies = new[]
        {
            Assembly.GetExecutingAssembly(),
            typeof(Bootstrap).Assembly
        };

        foreach (var assembly in assemblies)
        {
            var xmlFile = $"{assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath, true);
            }
        }

        // 2. Add JWT Bearer Security Definition
        //
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Please enter JWT with Bearer into field. Example: \"Bearer {token}\"",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        // 3. Configure Swagger Docs for each API version
        //
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = $"SalaryInsights API {description.GroupName}",
            Description = "An ASP.NET Core WebAPI for Salary Insights",
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact
            {
                Name = "Danvic Wang",
                Url = new Uri("https://github.com/danvic712")
            }
        };

        if (description.ApiVersion == options.Value.DefaultApiVersion)
        {
            if (!string.IsNullOrWhiteSpace(info.Description))
            {
                info.Description += " ";
            }

            info.Description += "Default SalaryInsights API version.";
        }

        if (description.IsDeprecated)
        {
            if (!string.IsNullOrWhiteSpace(info.Description))
            {
                info.Description += " ";
            }

            info.Description += "This API version is deprecated.";
        }

        return info;
    }
}