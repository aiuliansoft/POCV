using Application.Commands.GenerateOtp;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests;

/// <summary>
/// The request necessary to generate an OTP
/// </summary>
public class CreateRequest
{
    /// <summary>
    /// The User unique identifier
    /// </summary>
    [Required]
    public Guid? UserId { get; set; }
    /// <summary>
    /// Timestamp
    /// </summary>
    [Required]
    public DateTime? Timestamp { get; set; }

    public GenerateOtpCommand ToCommand() =>
        new GenerateOtpCommand
        {
            UserId = UserId.GetValueOrDefault(),
            Timestamp = Timestamp.GetValueOrDefault(),
        };
}
