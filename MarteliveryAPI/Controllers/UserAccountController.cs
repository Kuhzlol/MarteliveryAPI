using AutoMapper;
using MarteliveryAPI.Data;
using MarteliveryAPI.Models;
using MarteliveryAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserAccountController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public UserAccountController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Get method for admin to get all users info
        [HttpGet("GetAllUsersInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersInfo()
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

        //Get method for user to get their own info
        [HttpGet("GetMyInfo")]
        [Authorize(Roles = "Admin, Customer, Carrier")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
                return NotFound("User not found");

            var userDTO = _mapper.Map<UserInfoDTO>(user);

            return Ok(userDTO);
        }

        //Put method for user to update their own info
        [HttpPut("UpdateMyInfo")]
        [Authorize(Roles = "Admin, Customer, Carrier")]
        public async Task<IActionResult> UpdateMyInfo(UserInfoDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("Model is empty");

            var user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
                return NotFound("User not found");

            //Check that the new date of birth is not in the future and that the user is at least 18 years old
            if (userDTO.DateOfBirth > DateOnly.FromDateTime(DateTime.Now) || userDTO.DateOfBirth.AddYears(18) > DateOnly.FromDateTime(DateTime.Now))
                return BadRequest("Invalid date of birth, user must have at least 18 years old");

            _mapper.Map(userDTO, user);

            //Username == email; Normalize username and email == email.ToUpper()
            user.UserName = userDTO.Email;
            user.NormalizedUserName = userDTO.Email.ToUpper();
            user.NormalizedEmail = userDTO.Email.ToUpper();

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
