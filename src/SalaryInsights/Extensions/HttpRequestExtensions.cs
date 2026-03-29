// -----------------------------------------------------------------------
// <copyright file= "HttpRequestExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-28 22:15
// Modified by:
// Description: Http request extensions
// -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace SalaryInsights.Extensions;

public static class HttpRequestExtensions
{
    /// <summary>
    /// Check if the request is a multipart request
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static bool IsMultipartContentType(this HttpRequest request)
    {
        return !string.IsNullOrEmpty(request.ContentType) &&
               request.ContentType.StartsWith("multipart/", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Get the boundary from the content type
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string? GetMultipartBoundary(this HttpRequest request)
    {
        var mediaType = MediaTypeHeaderValue.Parse(request.ContentType);
        return HeaderUtilities.RemoveQuotes(mediaType.Boundary).Value;
    }
}
