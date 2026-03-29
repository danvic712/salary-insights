using System;
using System.ClientModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Asp.Versioning;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SalaryInsights.Application;
using SalaryInsights.Application.Contracts;
using SalaryInsights.Domain.Contracts;
using SalaryInsights.Infrastructure;
using SalaryInsights.Infrastructure.Repositories;

namespace SalaryInsights.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, EFUnitOfWork>();

        services.AddHttpContextAccessor();
        services.AddScoped<ITenantContext, HttpTenantContext>();

        services.AddScoped<IFileStorageAppService, FileStorageAppService>();

        services.AddDbContext<SalaryInsightsDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SalaryInsights");
            options.UseNpgsql(connectionString,
                x => x.MigrationsHistoryTable("ef_migrations_history"));
        });

        return services;
    }

    public static IServiceCollection AddSupabaseAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var projectUrl = configuration["Supabase:ProjectUrl"]?.TrimEnd('/');
                if (string.IsNullOrWhiteSpace(projectUrl))
                    throw new InvalidOperationException("Missing configuration: Supabase:ProjectUrl");

                var audience = configuration["Supabase:Jwt:Audience"];
                if (string.IsNullOrWhiteSpace(audience))
                    throw new InvalidOperationException("Missing configuration: Supabase:Jwt:Audience");

                options.Authority = $"{projectUrl}/auth/v1";
                options.RequireHttpsMetadata = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"{projectUrl}/auth/v1",
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2),
                    ValidateIssuerSigningKey = true,
                    NameClaimType = "sub",
                    RoleClaimType = ClaimTypes.Role
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        if (context.Principal?.Identity is not ClaimsIdentity identity)
                            return Task.CompletedTask;

                        var role = identity.FindFirst("role")?.Value;
                        if (!string.IsNullOrWhiteSpace(role) &&
                            !identity.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == role))
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, role));
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

    public static IServiceCollection AddSalaryInsightsApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    public static IServiceCollection AddSalaryInsightsSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }

    public static IServiceCollection AddAI(this IServiceCollection services, IConfiguration configuration)
    {
        var endpoint = configuration["AI:AzureOpenAI:Endpoint"];
        var key = configuration["AI:AzureOpenAI:Key"];
        var deploymentName = configuration["AI:AzureOpenAI:DeploymentName"];

        if (string.IsNullOrWhiteSpace(endpoint))
            throw new InvalidOperationException("Missing configuration: AI:AzureOpenAI:Endpoint");
        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("Missing configuration: AI:AzureOpenAI:Key");
        if (string.IsNullOrWhiteSpace(deploymentName))
            throw new InvalidOperationException("Missing configuration: AI:AzureOpenAI:DeploymentName");

        var chatClient = new AzureOpenAIClient(
                new Uri(endpoint),
                new ApiKeyCredential(key))
            .GetChatClient(deploymentName)
            .AsIChatClient()
            .AsBuilder()
            .UseOpenTelemetry(sourceName: "SalaryInsights", configure: (cfg) => cfg.EnableSensitiveData = true)
            .Build();

        services.AddChatClient(chatClient);
        services.AddSalaryInsightsAgents(chatClient);

        services.AddOpenAIResponses();
        services.AddOpenAIConversations();

        return services;
    }
}