using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.User;
using MarteliveryAPI.Services.EmailServices;
using MarteliveryAPI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

            //Send email confirmation to user email address if account is created successfully
            if (response.Flag == true)
            {
                var user = await _userManager.FindByEmailAsync(userDTO.Email);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = userDTO.Email }, Request.Scheme);

                var message = new Message(new string[] { userDTO.Email }, "Confirm your Email", confirmationLink, null);
                await _emailSender.SendEmailAsync(message);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Ok(result.Succeeded ? "Your Email is now confirmed, Welcome to Martelivery !" : "Error");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            var response = await user.LoginAccount(loginDTO);
            return Ok(response);
        }
    }
}
