using System.Reflection;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalaryInsights.Application;
using SalaryInsights.Application.Behaviors;
using SalaryInsights.Application.Contracts;
using SalaryInsights.Domain.Contracts;
using SalaryInsights.Infrastructure;
using SalaryInsights.Infrastructure.Exceptions;
using SalaryInsights.Infrastructure.Repositories;
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
    .WriteTo.Async(c => c.File("Logs/log-.txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Async(c => c.Console());

Log.Logger = loggerConfiguration.CreateLogger();

try
{
    Log.Information("Starting SalaryInsights.API");

    var builder = WebApplication.CreateBuilder(args);

    builder.AddSalaryInsights();

    builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<ITenantContext, HttpTenantContext>();

    var assembly = typeof(Bootstrap).GetTypeInfo().Assembly;

    // MediatR
    //
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    // FluentValidation
    builder.Services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

    builder.Services.AddDbContextPool<SalaryInsightsDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("SalaryInsights")));

    var signingKey = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!);

    // Configure JWT Bearer authentication.
    // This setup validates access tokens issued by Supabase (GoTrue) using OIDC discovery + JWKS.
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            // Read Supabase project URL (e.g. https://<project-ref>.supabase.co)
            // Trim trailing '/' to keep URL composition consistent.
            var projectUrl = builder.Configuration["Supabase:ProjectUrl"]?.TrimEnd('/');
            if (string.IsNullOrWhiteSpace(projectUrl))
                throw new InvalidOperationException("Missing configuration: Supabase:ProjectUrl");

            // Read expected JWT audience.
            // Supabase commonly uses "authenticated" as the default audience for user access tokens.
            var audience = builder.Configuration["Supabase:Jwt:Audience"];
            if (string.IsNullOrWhiteSpace(audience))
                throw new InvalidOperationException("Missing configuration: Supabase:Jwt:Audience");

            // Supabase GoTrue OIDC base address.
            // The middleware will use this to fetch the OpenID Connect discovery document
            // (/.well-known/openid-configuration) and the JWKS keys used to validate token signatures.
            options.Authority = $"{projectUrl}/auth/v1";

            // Require HTTPS when downloading metadata (recommended for production).
            options.RequireHttpsMetadata = true;

            // Token validation rules applied to incoming JWTs.
            options.TokenValidationParameters = new TokenValidationParameters
            {
                // Validate that the token was issued by the expected issuer (iss claim).
                ValidateIssuer = true,
                ValidIssuer = $"{projectUrl}/auth/v1",

                // Validate that the token audience (aud claim) matches what this API expects.
                ValidateAudience = true,
                ValidAudience = audience,

                // Validate token expiry and "not before" timestamps (exp/nbf).
                // ClockSkew provides a small tolerance window for server clock drift / network latency.
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2),

                // Ensure the token signature is validated (keys are resolved via Authority/JWKS).
                ValidateIssuerSigningKey = true,

                // Configure how ASP.NET Core maps identity claims:
                // - Use "sub" as the user identifier (Name).
                // - Use ClaimTypes.Role as the role claim type for role-based authorization.
                NameClaimType = "sub",
                RoleClaimType = ClaimTypes.Role
            };

            // Supabase typically stores the user's role in the "role" claim (e.g. "authenticated").
            // If you use custom roles in app_metadata (e.g. app_metadata.roles), you can map them here instead.
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    // Ensure we can mutate the claims identity.
                    if (context.Principal?.Identity is not ClaimsIdentity identity)
                        return Task.CompletedTask;

                    // Map Supabase "role" claim -> ClaimTypes.Role
                    // This makes [Authorize(Roles = "...")] work and supports checks like IsSuperAdmin.
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

    // Add services to the container.
    builder.Services.AddControllers();

    // Problem details and exception handler
    //
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    var app = builder.Build();

    app.MapDefaultEndpoints();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    if (ex is HostAbortedException)
    {
        throw;
    }

    Log.Fatal(ex, "SalaryInsights.API terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}