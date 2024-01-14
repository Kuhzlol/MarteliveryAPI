using MarteliveryAPI.Data;
using MarteliveryAPI.Entities;
using MarteliveryAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        //Get method for admin to get all users info
        [HttpGet("GetAllUserInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserInfo()
        {
            var users = await _context.Users.ToListAsync();

            if (users.Count == 0)
                return NotFound("Users not found");

            return Ok(users);
        }

        //Get method for admin to get user info by id
        [HttpGet("GetUserInfo/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserInfo(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        //Get method for any kind of user to get their own info
        [HttpGet("GetMyInfo")]
        [Authorize(Roles = "Admin, Customer, Carrier")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        //Put method for any kind of user to update their own info
        [HttpPut("UpdateMyInfo")]
        [Authorize(Roles = "Admin, Customer, Carrier")]
        public async Task<IActionResult> UpdateMyInfo(User user)
        {
            if (user == null)
                return BadRequest("Model is empty");

            var userToUpdate = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (userToUpdate == null)
                return NotFound("User not found");

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.DateOfBirth = user.DateOfBirth;
            userToUpdate.Email = user.Email;
            userToUpdate.PhoneNumber = user.PhoneNumber;

            await _context.SaveChangesAsync();

            return Ok("User info updated");
        }

        //Delete method for admin to delete user by id
        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User deleted");
        }

    }
}
