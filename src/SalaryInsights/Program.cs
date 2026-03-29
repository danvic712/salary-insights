using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalaryInsights.Extensions;
using SalaryInsights.Infrastructure.Exceptions;
using Serilog;
using Serilog.Events;

var loggerConfiguration = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Debug()
#else
    .MinimumLevel.Information()
#endif
    .MinimumLevel.Override(nameof(Microsoft), LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File("logs/log-.txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Async(c => c.Console());

Log.Logger = loggerConfiguration.CreateLogger();

try
{
    Log.Information("Starting SalaryInsights");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddSupabaseAuthentication(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddSalaryInsightsApiVersioning();
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddOpenApi();
    builder.Services.AddSalaryInsightsSwagger();
    builder.Services.AddAI(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSalaryInsightsSwagger();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.MapEndpoints();

    await app.RunAsync();
}
catch (Exception ex)
{
    if (ex is HostAbortedException)
    {
        throw;
    }

    Log.Fatal(ex, "SalaryInsights terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}