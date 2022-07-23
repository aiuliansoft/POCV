using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Persistance.Repositories
{
    /// <inheritdoc/>
    internal class OtpRepository : IOtpRepository
    {
        private readonly OtpContext _context;
        private readonly OtpConfiguration _otpConfiguration;

        public OtpRepository(OtpContext context, OtpConfiguration otpConfiguration)
        {
            _context = context;
            _otpConfiguration = otpConfiguration;
        }

        /// <inheritdoc/>
        public async Task CreateAsync(Otp otp)
        {
            await _context.Otp.AddAsync(otp);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<Otp?> GetAsync(Guid userId, int code) =>
            await _context
                .Otp
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Code == code);

        /// <inheritdoc/>
        public async Task InvalidateAsync(Guid userId, int code)
        {
            var otp = await _context
                .Otp
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Code == code);

            if(otp != null)
            {
                //Invalidates the token
                otp.Timestamp = DateTime.UtcNow.AddSeconds(-_otpConfiguration.Validity);
                await _context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task CleanUpAsync()
        {
            // Even though was not in the initial requirements I would go for it
            // This methdod would be called by a nigtly job (once in a month for instance, for now does nothing :D
            var expiredOtps = _context.Otp.Where(x => x.Timestamp < DateTime.UtcNow);
            _context.Otp.RemoveRange(expiredOtps);
            await _context.SaveChangesAsync();
        }

    }
}
