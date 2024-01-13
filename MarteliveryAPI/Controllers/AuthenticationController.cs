using MarteliveryAPI.DTOs;
using MarteliveryAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController(IUserRepository user) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO userDTO)
        {
            var response = await user.CreateAccount(userDTO);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            var response = await user.LoginAccount(loginDTO);
            return Ok(response);
        }
    }
}
