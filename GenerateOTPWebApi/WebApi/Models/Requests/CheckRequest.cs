using Application.Commands.CheckOtp;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests;

/// <summary>
/// The request necessary validate an OTP
/// </summary>
public class CheckRequest
{
    /// <summary>
    /// The User unique identifier
    /// </summary>
    [Required]
    public Guid? UserId { get; set; }
    /// <summary>
    /// OTP code
    /// </summary>
    [Required]
    public int? OTPCode { get; set; }

    public CheckOtpCommand ToCommand() =>
       new CheckOtpCommand
       {
           UserId = UserId.GetValueOrDefault(),
           Code = OTPCode.GetValueOrDefault(),
       };
}
