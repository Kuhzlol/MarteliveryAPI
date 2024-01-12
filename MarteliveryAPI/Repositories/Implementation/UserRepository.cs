using MarteliveryAPI.DTOs;
using MarteliveryAPI.Entities;
using MarteliveryAPI.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static MarteliveryAPI.DTOs.ServiceResponses;

namespace MarteliveryAPI.Repositories.Implementation
{
    public class UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserRepository
    {
        public async Task<GeneralResponse> CreateAccount(UserRegisterDTO userDTO)
        {
            if (userDTO is null)
                return new GeneralResponse(false, "Model is empty");

            var newUser = new User()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                DateOfBirth = userDTO.DateOfBirth,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null)
                return new GeneralResponse(false, "User already registered");

            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded)
                return new GeneralResponse(false, "Error occured.. please try again");

            //Assign Default Role : Admin to first registered user; rest is user
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
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);

            var userSession = new UserSession(getUser.Id, getUser.FirstName, getUser.LastName, getUser.Email, getUserRole.First());

            string token = GenerateToken(userSession);

            return new LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSession user)
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
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
