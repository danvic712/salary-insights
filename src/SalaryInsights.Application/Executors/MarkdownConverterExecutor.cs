// -----------------------------------------------------------------------
// <copyright file= "MarkdownConverterExecutor.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-22 18:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

namespace SalaryInsights.Application.Executors;

public class MarkdownConverterExecutor(IChatClient chatClient) : Executor<string>("MarkdownConverter")
{
    private readonly AIAgent _agent = new ChatClientAgent(chatClient, new ChatClientAgentOptions
    {
        Name = "markdown-converter",
        ChatOptions = new ChatOptions
        {
            Instructions = "你是一个高效的 markdown 转换器，能够将各种格式的文件和 URL 转换为 markdown 格式。",
        },
#pragma warning disable MAAI001
        AIContextProviders = [new FileAgentSkillsProvider(skillPath: Path.Combine(AppContext.BaseDirectory, "skills"))]
#pragma warning restore MAAI001
    });

    public override async ValueTask HandleAsync(string message, IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        var result = await _agent.RunAsync(message, cancellationToken: cancellationToken);

        // Send a turn token to signal the agent to process the accumulated messages
        await context.SendMessageAsync(new TurnToken(emitEvents: true), cancellationToken: cancellationToken);
    }
}