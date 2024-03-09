// -----------------------------------------------------------------------
// <copyright file= "EntityNotFoundException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2024-2-3 19:9
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Exceptions;

public class EntityNotFoundException : SIException
{
    public EntityNotFoundException(string resource, Guid entityId)
    {
        Resource = resource;
        EntityId = entityId.ToString();
    }

    public EntityNotFoundException(string resource, string entityId)
    {
        Resource = resource;
        EntityId = entityId;
    }

    public string Resource { get; set; }

    public string EntityId { get; set; }

    public new string Message =>
        $"{Resource} with key {EntityId} {(EntityId.Contains(',') ? "were" : "was")} not found.";
}