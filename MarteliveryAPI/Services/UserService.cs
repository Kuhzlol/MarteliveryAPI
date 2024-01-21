using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.User;
using MarteliveryAPI.Services.Interfaces;
using MarteliveryAPI.Services.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static MarteliveryAPI.Services.Options.UserResponseOption;

namespace MarteliveryAPI.Services
{
    public class UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUser
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
                UserName = userDTO.Email,
                IsCustomer = userDTO.IsCustomer
            };

            //Check that the date of birth is not in the future and that the user is at least 18 years old
            if (newUser.DateOfBirth > DateOnly.FromDateTime(DateTime.Now) || newUser.DateOfBirth.AddYears(18) > DateOnly.FromDateTime(DateTime.Now))
                return new GeneralResponse(false, "Invalid date of birth, user must have at least 18 years old");

            //Check if user already exists
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null)
                return new GeneralResponse(false, "User already registered");

            //Check if password is valid (contains at least 1 uppercase, 1 lowercase, 1 digit, 1 non-alphanumeric, and at least 8 characters)
            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded)
                return new GeneralResponse(false, "Invalid password.. Password must contain at least 1 uppercase, 1 lowercase, 1 digit, 1 non-alphanumeric, and at least 8 characters. Please try again");

            //Assign Default Role : "Admin" to first registered user; Assign "Customer" if IsCustomer is true, else assign "Carrier"
            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            var checkCustomer = await roleManager.FindByNameAsync("Customer");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");

                return new GeneralResponse(true, "Account Created");
            }
            else if (checkCustomer is null || newUser.IsCustomer == true)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Customer" });
                await userManager.AddToRoleAsync(newUser, "Customer");

                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkCarrier = await roleManager.FindByNameAsync("Carrier");
                if (checkCarrier is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "Carrier" });

                await userManager.AddToRoleAsync(newUser, "Carrier");

                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<LoginResponse> LoginAccount(UserLoginDTO loginDTO)
        {
            //Check if login container is empty
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            //Check if email match with user email in database
            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            //Check if password match with user password hash in database
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

            //Reset accessFailedCount if user successfully logged in before being locked out
            await userManager.ResetAccessFailedCountAsync(getUser);

            //Get user role
            var getUserRole = await userManager.GetRolesAsync(getUser);

            //Create user session
            var userSession = new UserSessionOption(getUser.Id, getUser.FirstName, getUser.LastName, getUser.Email, getUserRole.First());

            //Generate token
            string token = GenerateToken(userSession);

            // Return token if login is successful
            return new LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSessionOption user)
        {
            var keyVaultURL = new Uri(config.GetSection("KeyVaultURL").Value!);
            var credential = new DefaultAzureCredential();
            var client = new SecretClient(keyVaultURL, credential);
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(client.GetSecret("JWTKey").Value.Value.ToString()));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384);

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
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
