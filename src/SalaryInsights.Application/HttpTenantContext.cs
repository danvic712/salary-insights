// -----------------------------------------------------------------------
// <copyright file= "HttpTenantContext.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-13 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SalaryInsights.Application.Contracts;

namespace SalaryInsights.Application;

public class HttpTenantContext(IHttpContextAccessor httpContextAccessor) : ITenantContext
{
    public Guid UserId =>
        Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirst("sub")!.Value);

    public bool IsSuperAdmin =>
        httpContextAccessor.HttpContext!.User.Claims.Any(c => c is { Type: ClaimTypes.Role, Value: "super_admin" });

    public Guid? CurrentTenantId
    {
        get
        {
            if (!IsSuperAdmin)
                return UserId;

            var header = httpContextAccessor.HttpContext!
                .Request.Headers["X-Tenant-Id"]
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(header))
                return null;

            return Guid.Parse(header);
        }
    }
}