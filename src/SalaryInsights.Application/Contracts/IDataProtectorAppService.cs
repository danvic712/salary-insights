// -----------------------------------------------------------------------
// <copyright file= "IDataProtectorAppService.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-02-24 21:02
// Modified by:
// Description:
// -----------------------------------------------------------------------

namespace SalaryInsights.Application.Contracts;

public interface IDataProtectorAppService
{
    /// <summary>
    /// Encrypt any string data
    /// </summary>
    /// <param name="plainText">Plain text</param>
    /// <returns>Encrypted string</returns>
    string Protect(string plainText);

    /// <summary>
    /// Decrypt any string data
    /// </summary>
    /// <param name="cipherText">Encrypted string</param>
    /// <returns>Original plain text</returns>
    string Unprotect(string cipherText);
}