// -----------------------------------------------------------------------
// <copyright file= "MsgReaderTool.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-25 21:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.ComponentModel;
using MsgReader.Mime;

namespace SalaryInsights.Application.Tools;

public sealed class MsgReaderTool
{
    [Description("Convert an email file to a temporary HTML file.")]
    public static string ConvertToHtml(
        [Description("The path to the email file to convert.")]
        string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("FilePath cannot be null or whitespace", nameof(filePath));

        var fileName = Path.GetFileName(filePath);

        var fileInfo = new FileInfo(filePath);
        var msg = Message.Load(fileInfo);

        var body = msg.HtmlBody.GetBodyAsText();

        var tempFilePath = Path.Combine(Path.GetTempPath(), $"{fileName}.html");

        File.WriteAllText(tempFilePath, body);

        return tempFilePath;
    }
}