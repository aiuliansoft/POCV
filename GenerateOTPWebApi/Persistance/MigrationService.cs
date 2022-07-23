using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Persistance;

public class MigrationService
{
    private const int CommandTimeoutInSeconds = 60;

    private readonly ILogger<MigrationService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MigrationService(ILogger<MigrationService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task MigrateAsync()
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            try
            {
                using (var context = serviceProvider.GetRequiredService<OtpContext>())
                {
                    _logger.LogTrace("Otp Database Migration Started");
                    context.Database.SetCommandTimeout(CommandTimeoutInSeconds);
                    await context.Database.MigrateAsync();
                    _logger.LogTrace("Otp Database Migration Completed");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Exception while migrating Otp database");
                throw;
            }
        }
    }
}
