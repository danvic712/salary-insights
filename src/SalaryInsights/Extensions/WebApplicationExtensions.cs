using Asp.Versioning.ApiExplorer;
using Microsoft.Agents.AI.DevUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SalaryInsights.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseSalaryInsightsSwagger(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return app;
        
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapOpenAIResponses();
        app.MapOpenAIConversations();

        if (app.Environment.IsDevelopment())
        {
            app.MapDevUI();
        }

        app.MapControllers();

        return app;
    }
}