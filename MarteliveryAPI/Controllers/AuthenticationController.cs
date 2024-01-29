using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.User;
using MarteliveryAPI.Services.EmailServices;
using MarteliveryAPI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IUser user, UserManager<User> userManager, IEmailSender emailSender) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IEmailSender _emailSender = emailSender;
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO userDTO)
        {
            var response = await user.CreateAccount(userDTO);

            //Send confirmation mail to the user email address if his account is created successfully
            if (response.Flag == true)
            {
                var user = await _userManager.FindByEmailAsync(userDTO.Email);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = userDTO.Email }, Request.Scheme);

                string body = string.Empty;
                using (StreamReader reader = new StreamReader("wwwroot/html/EmailConfirmation.html"))
                {
                    body = await reader.ReadToEndAsync();
                }
                body = body.Replace("{ConfirmationLink}", confirmationLink);
                body = body.Replace("{UserName}", userDTO.Email);
                

                var message = new Message(new string[] { userDTO.Email }, "Account Confirmation", body, null);
                await _emailSender.SendEmailAsync(message);

                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            var response = await user.LoginAccount(loginDTO);
            return Ok(response);
        }

        
        /// <summary>
        /// Resend confirmation link to the user email address
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST Authentication/ResendConfirmationLink
        ///     {
        ///         "email": "John.doe@gmail.com"
        ///     }
        /// </remarks>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <response code="200">Email sent</response>
        /// <response code="400">User already registered</response>
        [HttpPost("ResendConfirmationLink")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResendConfirmationLink(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("User already registered");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = email }, Request.Scheme);

            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/html/EmailConfirmation.html"))
            {
                body = await reader.ReadToEndAsync();
            }
            body = body.Replace("{ConfirmationLink}", confirmationLink);
            body = body.Replace("{UserName}", email);

            var message = new Message(new string[] { email }, "Account Confirmation", body, null);
            await _emailSender.SendEmailAsync(message);

            return Ok("Email sent");
        }

        [HttpGet("ConfirmEmail")]
        //Hide this method from Swagger UI
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Ok(result.Succeeded ? "Your Email is now confirmed, Welcome to Martelivery !" : "Error");
        }
    }
}
