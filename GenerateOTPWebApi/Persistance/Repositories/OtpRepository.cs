using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Persistance.Repositories
{
    /// <inheritdoc/>
    internal class OtpRepository : IOtpRepository
    {
        private readonly OtpContext _context;

        public OtpRepository(OtpContext context)
        {
            _context = context;
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
        public async Task CleanUpAsync()
        {
            // Even though was not in the initial requirements I would go for it
            // This methdod would be called by a nigtly job, for now does nothing :D
            var expiredOtps = _context.Otp.Where(x => x.Timestamp < DateTime.UtcNow);
            _context.Otp.RemoveRange(expiredOtps);
            await _context.SaveChangesAsync();
        }
    }
}
