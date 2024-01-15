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

        //Get method for user to get their own info
        [HttpGet("GetMyInfo")]
        [Authorize(Roles = "Admin, Customer, Carrier")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
                return NotFound("User not found");

            var userDTO = _mapper.Map<UserMinimalInfoDTO>(user);

            return Ok(userDTO);
        }

        //Put method for user to update their own info
        [HttpPut("UpdateMyIn")]
        [Authorize(Roles = "Admin, Customer, Carrier")]
        public async Task<IActionResult> UpdateMyInfo(UserMinimalInfoDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("Model is empty");

            var userToUpdate = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (userToUpdate == null)
                return NotFound("User not found");

            userToUpdate = _mapper.Map(userDTO, userToUpdate);

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
