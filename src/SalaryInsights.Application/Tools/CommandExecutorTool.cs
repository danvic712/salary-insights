// -----------------------------------------------------------------------
// <copyright file= "CommandExecutorTool.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-23 21:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using SalaryInsights.Domain.Dtos;

namespace SalaryInsights.Application.Tools;

public sealed class CommandExecutorTool
{
    private static readonly HashSet<string> AllowedCommands = new(StringComparer.OrdinalIgnoreCase)
    {
        "uvx",
    };

    [Description("Execute a command with optional arguments.")]
    public static async Task<CommandExecutionResult> ExecuteCommandAsync(
        [Description("The command to execute.")]
        string command,
        [Description("Optional arguments to pass to the command.")]
        IEnumerable<string>? arguments = null,
        [Description("Timeout in milliseconds for command execution.")]
        int timeoutMilliseconds = 60_000,
        [Description("Optional cancellation token.")]
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command))
            throw new ArgumentException("Command cannot be null or whitespace.", nameof(command));

        var args = arguments?.ToList() ?? [];

        // First attempt: direct execution (fast path)
        var result = await TryExecuteAsync(command, args, timeoutMilliseconds, cancellationToken);

        // If command not found or failed to start → fallback to bash
        if (IsCommandNotFound(result))
        {
            result = await ExecuteWithShellAsync(command, args, timeoutMilliseconds, cancellationToken);
        }

        if (result.ExitCode != 0)
        {
            throw new InvalidOperationException(
                $"Command failed.\nExitCode: {result.ExitCode}\nError: {result.StandardError}");
        }

        return result;
    }

    /// <summary>
    /// Try executing command directly without shell.
    /// This is faster and safer if the binary exists in PATH.
    /// </summary>
    private static async Task<CommandExecutionResult> TryExecuteAsync(
        string command,
        List<string> args,
        int timeoutMilliseconds,
        CancellationToken cancellationToken)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = command,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // IMPORTANT: Use ArgumentList instead of string concatenation
        // This avoids quoting issues and argument parsing bugs
        foreach (var arg in args)
        {
            startInfo.ArgumentList.Add(arg);
        }

        return await RunProcessAsync(startInfo, timeoutMilliseconds, cancellationToken);
    }

    /// <summary>
    /// Fallback execution using bash shell.
    /// This helps when command relies on shell features, PATH, alias, or shims.
    /// </summary>
    private static async Task<CommandExecutionResult> ExecuteWithShellAsync(
        string command,
        List<string> args,
        int timeoutMilliseconds,
        CancellationToken cancellationToken)
    {
        var fullCommand = BuildShellCommand(command, args);

        var startInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{fullCommand}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        return await RunProcessAsync(startInfo, timeoutMilliseconds, cancellationToken);
    }

    /// <summary>
    /// Core process runner with timeout + async output handling.
    /// </summary>
    private static async Task<CommandExecutionResult> RunProcessAsync(
        ProcessStartInfo startInfo,
        int timeoutMilliseconds,
        CancellationToken cancellationToken)
    {
        using var process = new Process();
        process.StartInfo = startInfo;
        process.EnableRaisingEvents = true;

        if (!process.Start())
            throw new InvalidOperationException("Failed to start process.");

        var outputTask = process.StandardOutput.ReadToEndAsync(cancellationToken);
        var errorTask = process.StandardError.ReadToEndAsync(cancellationToken);

        var exitTask = process.WaitForExitAsync(cancellationToken);
        var completedTask = await Task.WhenAny(exitTask, Task.Delay(timeoutMilliseconds, cancellationToken));

        if (completedTask != exitTask)
        {
            try
            {
                // Kill entire process tree on timeout
                if (!process.HasExited)
                    process.Kill(entireProcessTree: true);
            }
            catch
            {
                // Ignore kill failures
            }

            throw new TimeoutException($"Command execution timed out after {timeoutMilliseconds} ms.");
        }

        await Task.WhenAll(outputTask, errorTask);

        var output = await outputTask;
        var error = await errorTask;

        return new CommandExecutionResult(
            ExitCode: process.ExitCode,
            StandardOutput: output,
            StandardError: error);
    }

    /// <summary>
    /// Build a safe shell command string.
    /// Only used when falling back to bash.
    /// </summary>
    private static string BuildShellCommand(string command, List<string> args)
    {
        var sb = new StringBuilder();
        sb.Append(command);

        foreach (var arg in args)
        {
            sb.Append(' ');
            sb.Append(EscapeShellArgument(arg));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Escapes shell arguments to reduce injection risks.
    /// </summary>
    private static string EscapeShellArgument(string arg)
    {
        if (string.IsNullOrEmpty(arg))
            return "\"\"";

        // Basic escaping for bash
        return $"\"{arg.Replace("\"", "\\\"")}\"";
    }

    /// <summary>
    /// Detect if the command failed due to "not found" or similar issues.
    /// </summary>
    private static bool IsCommandNotFound(CommandExecutionResult result)
    {
        if (result.ExitCode == 0)
            return false;

        var error = result.StandardError?.ToLowerInvariant() ?? string.Empty;

        return error.Contains("not found") ||
               error.Contains("no such file") ||
               error.Contains("cannot find");
    }
}