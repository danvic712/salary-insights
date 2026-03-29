namespace SalaryInsights.Domain.Dtos;

public sealed record CommandExecutionResult(
    int ExitCode,
    string StandardOutput,
    string StandardError);