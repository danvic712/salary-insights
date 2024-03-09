// -----------------------------------------------------------------------
// <copyright file= "ICurrentUser.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-3-9 9:34
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Applications.Shared.Contracts;

public interface ICurrentUser
{
    /// <summary>
    /// 
    /// </summary>
    Guid UserId { get; }
    
    /// <summary>
    /// 
    /// </summary>
    string? UserName { get; }
    
    /// <summary>
    /// 
    /// </summary>
    string? Email { get; }
    
    /// <summary>
    /// 
    /// </summary>
    Task<IList<string>> GetRolesAsync();
}