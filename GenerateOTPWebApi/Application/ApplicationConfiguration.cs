using Application.Commands;
using Application.Commands.CheckOtp;
using Application.Commands.GenerateOtp;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection RegisterHandlers(this IServiceCollection services) =>
            services
                .AddScoped<ICommandHandler<CheckOtpCommand, bool>, CheckOtpCommandHandler>()
                .AddScoped<ICommandHandler<GenerateOtpCommand, Otp>, GenerateOtpCommandHandler>();
    }
}
