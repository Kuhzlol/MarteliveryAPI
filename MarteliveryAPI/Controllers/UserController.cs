using AutoMapper;
using MarteliveryAPI.Data;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(DataContext context, IMapper mapper) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        /*---------*/
        /*  ADMIN  */
        /*---------*/

        //Get method for admin to get all users info with Mapped DTO
        [HttpGet("AdminGetAllUsersInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetAllUsersInfo()
        {
            //Order by last created user first
            var users = await _context.Users.OrderByDescending(u => u.CreatedOn).ToListAsync();

            if (users.Count == 0)
                return NotFound("Users not found");

            var usersDTO = _mapper.Map<List<AdminUserInfoDTO>>(users);

            return Ok(usersDTO);
        }

        //Get method for admin to get user info by id with Mapped DTO
        [HttpGet("AdminGetUserInfo/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetUserInfo(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found");

            var userDTO = _mapper.Map<AdminUserInfoDTO>(user);

            return Ok(userDTO);
        }

        //Put method for admin to update user by id with Mapped DTO
        [HttpPut("AdminUpdateUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateUser(string id, AdminUpdateUserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("Model is empty");

            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found");

            //Check if email is already taken
            var checkEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            if (checkEmail != null && checkEmail.Id != user.Id)
                return BadRequest("Email already taken");

            user = _mapper.Map(userDTO, user);

            //Username == email; Normalize username and email == email.ToUpper()
            user.UserName = userDTO.Email;
            user.NormalizedUserName = userDTO.Email.ToUpper();
            user.NormalizedEmail = userDTO.Email.ToUpper();

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("User info updated");
        }

        //Delete method for admin to delete user by id
        [HttpDelete("AdminDeleteUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User deleted");
        }

        /*---------*/
        /*  USERS  */
        /*---------*/

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
        public async Task<IActionResult> UpdateMyInfo(UserInfoUpdateDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("Model is empty");

            var user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
                return NotFound("User not found");

            //Check that the date of birth is not in the future and that the user is at least 18 years old
            if (userDTO.DateOfBirth > DateOnly.FromDateTime(DateTime.Now) || userDTO.DateOfBirth.AddYears(18) > DateOnly.FromDateTime(DateTime.Now))
                return BadRequest("Invalid date of birth, user must have at least 18 years old");

            //Check if email is already taken
            var checkEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            if (checkEmail != null && checkEmail.Id != user.Id)
                return BadRequest("Email already taken");

            user = _mapper.Map(userDTO, user);

            //Username == email; Normalize username and email == email.ToUpper()
            user.UserName = userDTO.Email;
            user.NormalizedUserName = userDTO.Email.ToUpper();
            user.NormalizedEmail = userDTO.Email.ToUpper();

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("User info updated");
        }

        //Put method for user to update their own password with Mapped DTO
        [HttpPut("UpdateMyPassword")]
        [Authorize(Roles = "Admin, Customer, Carrier")]
        public async Task<IActionResult> UpdateMyPassword(UserPasswordUpdateDTO userDTO, UserManager<User> userManager)
        {
            if (userDTO == null)
                return BadRequest("Model is empty");

            var user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
                return NotFound("User not found");

            user = _mapper.Map(userDTO, user);

            //Check if password is valid (contains at least 1 uppercase, 1 lowercase, 1 digit, 1 non-alphanumeric, and at least 8 characters)
            var passwordValidator = new PasswordValidator<User>();
            var passwordValidation = await passwordValidator.ValidateAsync(userManager, user, userDTO.Password);
            if (!passwordValidation.Succeeded)
                return BadRequest("Invalid password.. Password must contain at least 1 uppercase, 1 lowercase, 1 digit, 1 non-alphanumeric, and at least 8 characters. Please try again");

            //Hash the new password
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, userDTO.Password);

            // Generate a new security stamp for the user and update it in the database
            user.SecurityStamp = Guid.NewGuid().ToByteArray().Aggregate("", (s, b) => s + b.ToString("X2"));

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Password updated");
        }
    }
}
