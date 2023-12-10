// -----------------------------------------------------------------------
// <copyright file= "OperationResponse.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 13:6
// Modified by:
// Description: Standard resource operation return object
// -----------------------------------------------------------------------

namespace SalaryInsights.Shared.Responses;

public class OperationResponse<TPrimaryKey, TResource> : ResponseBase<TResource>
    where TResource : class
{
    #region Properties

    /// <summary>
    /// Operational data primary key
    /// </summary>
    public TPrimaryKey? Id { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; set; }

    #endregion Properties

    #region Methods

    public static OperationResponse<TPrimaryKey, TResource> Success(TPrimaryKey id, TResource resource)
    {
        return new OperationResponse<TPrimaryKey, TResource>()
        {
            Status = true,
            Id = id,
            Resource = resource
        };
    }

    public static OperationResponse<TPrimaryKey, TResource> Failure(
        string message = "You have encountered an unexpected error. Please contact the support for assistance.",
        Dictionary<string, IList<string>> errors = null,
        TPrimaryKey? id = default)
    {
        return new OperationResponse<TPrimaryKey, TResource>()
        {
            Status = false,
            Message = message,
            Id = id,
            Errors = errors
        };
    }

    #endregion
}