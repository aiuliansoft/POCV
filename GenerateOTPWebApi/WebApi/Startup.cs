using Application;
using Models;
using Persistance;
using WebApi.Configuration;
using WebApi.Middleware;

namespace WebApi;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers();

        services
            .ConfigureSwaggerGen()
            .AddSingleton(Configuration.GetSection("OtpConfiguration").Get<OtpConfiguration>())
            .AddPersistenceBindings(Configuration)
            .RegisterHandlers();

        services.AddCors();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
            );

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("v1/swagger.json", "OTP Api");
        });
    }
}
