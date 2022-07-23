using Application.Commands;
using Application.Commands.CheckOtp;
using Application.Commands.GenerateOtp;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.Models.Requests;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OtpController : ControllerBase
    {
        /// <summary>
        /// Create an OTP for a particular user with a predefined lifespan
        /// </summary>
        /// <param name="handler">The command handler that is responsible for the OTP generation</param>
        /// <param name="request">Necessary data used to create the OTP</param>
        /// <returns>An OTP object</returns>
        [HttpPost(Name = "CreateAsync")]
        public async Task<IActionResult> CreateAsync(
            [FromServices] ICommandHandler<GenerateOtpCommand, Otp> handler,
            [FromBody] CreateRequest request)
        {
            var result = await handler.ProcessCommandAsync(request.ToCommand());

            return Ok(result);
        }

        /// <summary>
        /// Checks if an OTP for a particular user is still valid
        /// </summary>
        /// <param name="handler">The command handler that is reponsible for checking the validity of a token</param>
        /// <param name="request">Necessary data used to check the OTP</param>
        /// <returns>A boolean</returns>
        [HttpPost("check", Name = "CheckAsync")]
        public async Task<IActionResult> CheckAsync(
            [FromServices] ICommandHandler<CheckOtpCommand, bool> handler,
            [FromBody] CheckRequest request)
        {
            var result = await handler.ProcessCommandAsync(request.ToCommand());

            return Ok(result);
        }
    }
}