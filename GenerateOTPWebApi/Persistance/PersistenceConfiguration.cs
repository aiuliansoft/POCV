using Application.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddPersistenceBindings(this IServiceCollection services, IConfiguration configuration) => services
        .AddDbContext<OtpContext>((provider, options) =>
        {
            options.UseSqlServer(new SqlConnection(ConfigurationExtensions.GetConnectionString(configuration, "Otp")));
        })
        .AddScoped<IOtpRepository, OtpRepository>()
        .AddSingleton<MigrationService>();
}
