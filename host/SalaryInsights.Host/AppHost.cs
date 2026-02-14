using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var postgresDatabase = postgres.AddDatabase("salary-insights");

var salaryInsights = builder.AddProject<SalaryInsights_API>("salary-insights-api")
    .WithReference(postgresDatabase)
    .WithHttpHealthCheck("/health");

await builder.Build().RunAsync();