// -----------------------------------------------------------------------
// <copyright file= "CurrentUser.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-3-9 9:35
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SalaryInsights.Applications.Shared.Contracts;

namespace SalaryInsights.Applications.Shared;

/// <summary>
/// 
/// </summary>
public class CurrentUser : ICurrentUser
{
    #region Initializes

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="userManager"></param>
    public CurrentUser(
        IHttpContextAccessor httpContextAccessor,
        UserManager<IdentityUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext?
        .User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

    /// <summary>
    /// 
    /// </summary>
    public string? UserName => _httpContextAccessor.HttpContext?
        .User.FindFirstValue(ClaimTypes.Name);

    /// <summary>
    /// 
    /// </summary>
    public string? Email => _httpContextAccessor.HttpContext?
        .User.FindFirstValue(ClaimTypes.Email);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IList<string>> GetRolesAsync()
    {
        var claims = _httpContextAccessor.HttpContext?.User;
        if (claims is null)
            return Enumerable.Empty<string>().ToList();

        var user = await _userManager.GetUserAsync(claims);
        if (user is null)
            return Enumerable.Empty<string>().ToList();

        return await _userManager.GetRolesAsync(user);
    }
}