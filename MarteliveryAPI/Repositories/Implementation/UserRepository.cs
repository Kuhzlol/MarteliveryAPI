using MarteliveryAPI.DTOs;
using MarteliveryAPI.Entities;
using MarteliveryAPI.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static MarteliveryAPI.Services.ResponseService;

namespace MarteliveryAPI.Repositories.Implementation
{
    public class UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserRepository
    {
        public async Task<GeneralResponse> CreateAccount(UserRegisterDTO userDTO)
        {
            if (userDTO is null)
                return new GeneralResponse(false, "Model is empty");

            //Check that the date of birth is not in the future and that the user is at least 18 years old
            if (userDTO.DateOfBirth > DateOnly.FromDateTime(DateTime.Now) || userDTO.DateOfBirth.AddYears(18) > DateOnly.FromDateTime(DateTime.Now))
                return new GeneralResponse(false, "Invalid date of birth, user must have at least 18 years old");

            var newUser = new User()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                DateOfBirth = userDTO.DateOfBirth,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };

            //Check if user already exists
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null)
                return new GeneralResponse(false, "User already registered");

            //Check if password is valid (contains at least 1 uppercase, 1 lowercase, 1 digit, 1 non-alphanumeric, and at least 8 characters)
            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded)
                return new GeneralResponse(false, "Invalid password.. Password must contain at least 1 uppercase, 1 lowercase, 1 digit, 1 non-alphanumeric, and at least 8 characters. Please try again");

            //Assign Default Role : "Admin" to first registered user; rest are "User"
            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");

                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await userManager.AddToRoleAsync(newUser, "User");

                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<LoginResponse> LoginAccount(UserLoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
            {
                //Increment accessFailedCount if password is incorrect
                await userManager.AccessFailedAsync(getUser);
                return new LoginResponse(false, null!, "Invalid email/password");
            }
            
            //Check if user is locked out
            var checkUserLockout = await userManager.IsLockedOutAsync(getUser);
            if (checkUserLockout)
                return new LoginResponse(false, null!, "User is locked out");

            var getUserRole = await userManager.GetRolesAsync(getUser);

            var userSession = new UserSessionRepository(getUser.Id, getUser.FirstName, getUser.LastName, getUser.Email, getUserRole.First());

            string token = GenerateToken(userSession);

            return new LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSessionRepository user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
