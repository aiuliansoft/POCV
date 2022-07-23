using Application.Repositories;
using Models;

namespace Application.Commands.GenerateOtp;

public class GenerateOtpCommandHandler : ICommandHandler<GenerateOtpCommand, Otp>
{
    public readonly IOtpRepository _otpRepository;
    private readonly OtpConfiguration _otpConfiguration;

    public GenerateOtpCommandHandler(IOtpRepository otpRepository, OtpConfiguration otpConfiguration)
    {
        _otpRepository = otpRepository ?? throw new ArgumentNullException(nameof(otpRepository));
        _otpConfiguration = otpConfiguration ?? throw new ArgumentNullException(nameof(otpConfiguration));
    }

    public async Task<Otp> ProcessCommandAsync(GenerateOtpCommand command)
    {
        var otp = new Otp
        {
            Code = new Random().Next(100000, 999999),
            //I would prefer to use a datetime generated on the server instead the one coming from the client
            Timestamp = DateTime.UtcNow,
            UserId = command.UserId,
            Validity = _otpConfiguration.Validity
        };

        await _otpRepository.CreateAsync(otp);

        return otp;
    }
}
