using MarteliveryAPI.Data;
using MarteliveryAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet("GetAllUserInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserInfo()
        {
            var users = await _context.Users.ToListAsync();

            if (users.Count == 0)
                return NotFound("Users not found");

            return Ok(users);
        }

        [HttpGet("GetUserInfo/{id}")]
        [Authorize(Roles = "Admin, Customer, Carrier")]
        public async Task<IActionResult> GetUserInfo(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

    }
}
