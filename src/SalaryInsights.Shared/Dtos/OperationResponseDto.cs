// -----------------------------------------------------------------------
// <copyright file= "OperationResponseDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2023-12-10 13:6
// Modified by:
// Description: Standard resource operation return object
// -----------------------------------------------------------------------

namespace SalaryInsights.Shared.Dtos;

public class OperationResponseDto<TPrimaryKey, TResource> : ResponseBaseDto<TResource>
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

    public static OperationResponseDto<TPrimaryKey, TResource> Success(TPrimaryKey id, TResource resource)
    {
        return new OperationResponseDto<TPrimaryKey, TResource>()
        {
            Status = true,
            Id = id,
            Resource = resource
        };
    }

    public static OperationResponseDto<TPrimaryKey, TResource> Failure(
        string message = "You have encountered an unexpected error. Please contact the support for assistance.",
        Dictionary<string, IList<string>> errors = null,
        TPrimaryKey? id = default)
    {
        return new OperationResponseDto<TPrimaryKey, TResource>()
        {
            Status = false,
            Message = message,
            Id = id,
            Errors = errors
        };
    }

    #endregion
}