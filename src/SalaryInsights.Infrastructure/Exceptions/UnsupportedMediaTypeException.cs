// -----------------------------------------------------------------------
// <copyright file= "UnsupportedMediaTypeException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 22:25
// Modified by:
// Description: Unsupported media type exception
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace SalaryInsights.Infrastructure.Exceptions;

public sealed class UnsupportedMediaTypeException(string message, string errorCode = "unsupported_media_type")
    : SIException(message, StatusCodes.Status415UnsupportedMediaType, errorCode)
{
    public override string Title => "Unsupported Media Type";
}
