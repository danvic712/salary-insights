// -----------------------------------------------------------------------
// <copyright file= "BaseManager.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-3-9 15:29
// Modified by:
// Description:
// -----------------------------------------------------------------------

using SalaryInsights.Applications.Shared.Contracts;

namespace SalaryInsights.Applications.Shared;

public class BaseManager
{
    #region Initializes

    protected readonly ICurrentUser _currentUser;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="currentUser"></param>
    protected BaseManager(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    #endregion
}