using MarteliveryAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using SharedClassLibrary.Contracts;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController(IUserRepository user) : ControllerBase
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
