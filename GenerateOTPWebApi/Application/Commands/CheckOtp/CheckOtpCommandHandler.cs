using Application.Repositories;
using Models;

namespace Application.Commands.CheckOtp;

public class CheckOtpCommandHandler : ICommandHandler<CheckOtpCommand, bool>
{
    public readonly IOtpRepository _otpRepository;
    private readonly OtpConfiguration _otpConfiguration;

    public CheckOtpCommandHandler(IOtpRepository otpRepository, OtpConfiguration otpConfiguration)
    {
        _otpRepository = otpRepository ?? throw new ArgumentNullException(nameof(otpRepository));
        _otpConfiguration = otpConfiguration ?? throw new ArgumentNullException(nameof(otpConfiguration));
    }

    public async Task<bool> ProcessCommandAsync(CheckOtpCommand command)
    {
        Otp? otp = await _otpRepository.GetAsync(command.UserId, command.Code);

        bool isValid = false;
        if(otp != null && otp.Timestamp.AddSeconds(_otpConfiguration.Validity) >= DateTime.UtcNow)
        {
            isValid = true;

            //Invalidates the token
            otp.Timestamp = DateTime.UtcNow.AddSeconds(-_otpConfiguration.Validity);
            await _otpRepository.UpdateAsync(otp);
        }

        return isValid;
    }
}
