using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SalaryInsights.Applications.Charts;
using SalaryInsights.Applications.Charts.Contracts;
using SalaryInsights.Applications.Companies;
using SalaryInsights.Applications.Companies.Contracts;
using SalaryInsights.Applications.Payrolls;
using SalaryInsights.Applications.Payrolls.Contracts;
using SalaryInsights.Applications.Shared;
using SalaryInsights.Applications.Shared.Contracts;
using Serilog;
using Serilog.Events;

namespace SalaryInsights
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
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
                var builder = WebApplication.CreateBuilder(args);

                ConfigureServices(builder);

                var app = builder.Build();

                using var scope = builder.Services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<SalaryInsightsDbContext>();
                await dbContext.Database.EnsureCreatedAsync();

                app.UseDefaultFiles();
                app.UseStaticFiles();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();

                    app.UseDeveloperExceptionPage();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                Log.Information("Starting SalaryInsights");

                app.MapGroup("/api").MapIdentityApi<IdentityUser>();

                app.MapControllers();

                app.MapFallbackToFile("/index.html");

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

            void ConfigureServices(WebApplicationBuilder builder)
            {
                // Add services to the container
                //
                builder.Services.AddControllers();
                builder.Services.AddProblemDetails();

                builder.Services.AddDbContext<SalaryInsightsDbContext>(options =>
                    options.UseSqlite(builder.Configuration.GetConnectionString(nameof(SalaryInsights))));

                builder.Services.AddDistributedMemoryCache();

                builder.Services.AddAutoMapper(typeof(MapperProfile));

                builder.Services.AddHttpContextAccessor();

                builder.Services.AddScoped<ICurrentUser, CurrentUser>();
                builder.Services.AddScoped<IChartManager, ChartManager>();
                builder.Services.AddScoped<ICompanyManager, CompanyManager>();
                builder.Services.AddScoped<IPayrollManager, PayrollManager>();

                builder.Services.AddAuthorization();

                // IdentityService
                //
                builder.Services
                    .AddIdentityApiEndpoints<IdentityUser>()
                    .AddEntityFrameworkStores<SalaryInsightsDbContext>();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                //
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "SalaryInsights API",
                        Description = "An ASP.NET Core Web API for managing SalaryInsights.",
                        Contact = new OpenApiContact
                        {
                            Name = "Danvic Wang",
                            Url = new Uri("https://github.com/danvic712")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                            Url = new Uri("https://github.com/danvic712/salary-insights/blob/main/LICENSE")
                        }
                    });

                    options.DescribeAllParametersInCamelCase();

                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });
            }
        }
    }
}