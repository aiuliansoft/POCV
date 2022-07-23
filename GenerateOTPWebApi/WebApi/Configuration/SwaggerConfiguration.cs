using Microsoft.OpenApi.Models;

namespace WebApi.Configuration;

public static class SwaggerConfiguration
{
    internal static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services) =>
        services.AddSwaggerGen(config =>
        {
            config.CustomSchemaIds(type => type.FullName);
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Vertiv OTP Api",
                Version = "1.0.0",
                Description = "This API offers support for handling OTPs"
            });

            var xmlFile = $"{typeof(SwaggerConfiguration).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            config.IncludeXmlComments(xmlPath);
        });
}
