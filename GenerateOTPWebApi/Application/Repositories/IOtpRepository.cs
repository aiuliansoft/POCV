using Models;

namespace Application.Repositories;

/// <summary>
/// Handles the database operations for the OTP
/// </summary>
public interface IOtpRepository
{
    /// <summary>
    /// Saves the OTP within the database
    /// </summary>
    Task CreateAsync(Otp otp);
    /// <summary>
    /// Gets the OTP for the requested user and code
    /// </summary>
    Task<Otp?> GetAsync(Guid userId, int code);
    /// <summary>
    /// Cleans up all the expired tokens
    /// </summary>
    Task CleanUpAsync();
}
