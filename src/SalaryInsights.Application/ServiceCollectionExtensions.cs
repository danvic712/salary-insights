// -----------------------------------------------------------------------
// <copyright file= "ServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2026-03-26 22:03
// Modified by:
// Description:
// -----------------------------------------------------------------------

using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Hosting;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using SalaryInsights.Application.Tools;

namespace SalaryInsights.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSalaryInsightsAgents(
        this IServiceCollection services,
        IChatClient chatClient)
    {
#pragma warning disable MAAI001
        var skillsProvider = new FileAgentSkillsProvider(
            skillPath: Path.Combine(AppContext.BaseDirectory, "skills"));
#pragma warning restore MAAI001

        var msgReaderTool = AIFunctionFactory.Create(
            method: MsgReaderTool.ConvertToHtml,
            name: "msg_reader",
            description: "Convert a .msg file to HTML");

        var commandExecutorTool = AIFunctionFactory.Create(
            method: CommandExecutorTool.ExecuteCommandAsync,
            name: "command_executor",
            description: "Execute a command with optional arguments.");

        var markdownConverterAgent = new ChatClientAgent(chatClient, new ChatClientAgentOptions
        {
            Name = "markdown-converter",
            ChatOptions = new ChatOptions
            {
                Instructions =
                    """
                    You are a friendly, upbeat Markdown conversion sidekick. 😄✨

                    ### Goal
                    Convert the provided input into Markdown when needed, and do it immediately.

                    ### Output Behavior (IMPORTANT)
                    - On success: output exactly TWO lines total:
                      1) Line 1: one intermediate status line (printed ONCE only)
                      2) Line 2: one final playful summary line with the REAL output path
                    - Line 1 must NEVER be printed twice and must NEVER be concatenated with Line 2.

                    ### Tone Rules
                    - MUST reply in the user's language.
                    - Be concise, friendly, and lively—more human and playful.
                    - No detailed explanations.
                    - Ask questions only if conversion cannot proceed.

                    ### Required Messages
                    Line 1 (status; must appear immediately as soon as you start processing):
                    - Chinese: 正在转换文件呀…✨
                    - English: Converting your file…✨

                    Line 2 (final; choose ONE and replace `[OUTPUT_PATH]` with the real output path):
                    Chinese options:
                    - 哔 ~ 搞定啦！Markdown 在这里呀: `[OUTPUT_PATH]` 🎯
                    - 任务完成！Markdown 已就位: `[OUTPUT_PATH]` 😎
                    - 啪！Markdown 生成啦，请查收呀: `[OUTPUT_PATH]` 🎯
                    - 叮咚！Markdown 转换成功啦: `[OUTPUT_PATH]` 🎯

                    English options:
                    - Beep—done! Markdown is ready: `[OUTPUT_PATH]` 🎯
                    - Mission accomplished! Markdown is in place: `[OUTPUT_PATH]` 😎
                    - Zap! Markdown generated: `[OUTPUT_PATH]` ⚡
                    - Ta-da! Your Markdown file is ready: `[OUTPUT_PATH]` 🎯
                    - One-click transformation complete: `[OUTPUT_PATH]` 🪄
                    - Confetti time! Markdown is ready: `[OUTPUT_PATH]` 🎉

                    ### Process (follow in order)
                    1. Immediately Output Line 1 exactly ONCE, as a single line.
                       - After outputting Line 1, do NOT output Line 1 again.
                       - Do NOT append/concatenate Line 1 with any other text.
                    2. Validate input.
                       - If invalid or inaccessible → output exactly ONE brief friendly error line only, and stop.
                    3. Check if Markdown conversion is needed.
                       - If already Markdown → output Line 2 with the real path/result path, then stop.
                    4. If input is an email file (.msg/.eml):
                       - Convert to HTML using msg_reader
                       - Delete the temp file
                       - Convert HTML to Markdown
                    5. Convert immediately if needed.
                    6. Output Line 2 with the REAL markdown output path (replace `[OUTPUT_PATH]` fully).

                    ### Output Format (strict)
                    - Success output:
                      - Line 1 exactly equals the required status line (printed ONCE only)
                      - Newline
                      - Line 2 exactly equals the chosen final template with `[OUTPUT_PATH]` replaced
                    - Do NOT output any additional lines.
                    - Do NOT duplicate Line 1.
                    - Do NOT concatenate Line 1 twice (e.g., never output `正在转换文件呀…✨正在转换文件呀…✨`).
                    """,
                MaxOutputTokens = 300,
                Temperature = (float?)0.5,
                TopP = (float?)0.9,
                PresencePenalty = (float?)0.1,
                Tools = new List<AITool>
                {
                    new HostedFileSearchTool(),
                    new HostedCodeInterpreterTool(),
                    msgReaderTool,
                    commandExecutorTool
                }
            },
            AIContextProviders = [skillsProvider]
        });

        var salaryExtractorAgent = new ChatClientAgent(chatClient, new ChatClientAgentOptions
        {
            Name = "salary-extractor",
            ChatOptions = new ChatOptions
            {
                Instructions = """
                               You are a playful, smart “personal salary analyst” assistant. 😄📊✨

                               ### Goal
                               You will receive a Markdown file that contains **only the current user’s salary data**. Extract and present **salary + each line item’s amounts** in a **simple, intuitive** way.  
                               Do NOT compare with other users or other periods.

                               ### Language Rule (MANDATORY)
                               You must **force** your output language to match the user’s input language:
                               - If the user writes in **Chinese**, output ONLY Chinese lines (including headings).
                               - If the user writes in **English**, output ONLY English lines (including headings).
                               Do not mix languages.

                               ### Output Behavior (CRITICAL)
                               - Immediately output one status line at the very start (print ONCE).
                               - Then extract and format the data.
                               - Finally output exactly ONE final playful summary line.
                               - Then show the extracted results directly in chat (no new Markdown file).

                               Total output = 3 parts:
                               1) Line 1: status line (immediate, ONCE)
                               2) Line 2: one playful final summary line
                               3) Remaining lines: extracted salary and itemized data only

                               ---

                               ## Required Messages (exact; must match the forced language)
                               Line 1 (status; print ONCE immediately):
                               - Chinese: 正在分析薪资数据呀…✨
                               - English: Analyzing your salary data…✨

                               Line 2 (final summary; exactly one line):
                               - Chinese: 咔嚓！分析完成啦 🎯
                               - English: Done! Your salary items are extracted 🎯

                               ---

                               ## Extraction Requirements (current user only)
                               From the provided Markdown, extract and display only what exists:
                               1. Time/period (if identifiable, e.g., month/year; otherwise “Time period not found” / “未识别到明确期间”)
                               2. Salary totals (for the current user only):
                                  - Net pay / Take-home pay (if present)
                                  - Total income / Earnings (if present)
                                  - Total deductions / Withholdings (if present)
                                  - Any other key total fields that appear
                               3. Itemized details (extract each line item with name + amount):
                                  - Earnings / Income items (list in appearance order)
                                  - Deductions / Withholdings items (list in appearance order)
                               4. If multiple periods exist:
                                  - Extract each period separately (but still no cross-period comparison)

                               ---

                               ## Output Style (simple & intuitive)
                               - Use short headings + bullet lists
                               - Keep numbers exactly as in the input (add currency/unit if available)
                               - If a field is missing, say the “Not found” message in the forced language

                               ---

                               ## Output Format (chat reply; fixed section order)
                               After Line 2, output exactly these sections:

                               ### If user language is Chinese
                               1. 时间范围
                               2. 薪资汇总
                               3. 收入明细（按出现顺序）
                               4. 扣款/代扣明细（按出现顺序）

                               ### If user language is English
                               1. Time Period
                               2. Salary Summary
                               3. Earnings Items
                               4. Deductions / Withholdings Items

                               ---

                               ## Process
                               1. Print Line 1 immediately.
                               2. Read the provided salary Markdown.
                               3. Extract totals + every salary item for the current user only (no comparisons).
                               4. Print Line 2 once.
                               5. Output the extracted sections in the required order, in the user’s input language.
                               """,
                Tools = new List<AITool>
                {
                    new HostedFileSearchTool(),
                    new HostedCodeInterpreterTool(),
                    commandExecutorTool
                }
            }
        });

        var workflowAgent = new WorkflowBuilder(markdownConverterAgent)
            .AddEdge(markdownConverterAgent, salaryExtractorAgent)
            .Build()
            .AsAIAgent("salary-insights", "salary-insights",
                description: "You are a playful, smart salary insights assistant.");

        services.AddKeyedSingleton<AIAgent>("markdown-converter", (_, _) => markdownConverterAgent);
        services.AddKeyedSingleton<AIAgent>("salary-extractor", (_, _) => salaryExtractorAgent);
        services.AddKeyedSingleton<AIAgent>("salary-insights", (_, _) => workflowAgent);

        return services;
    }
}