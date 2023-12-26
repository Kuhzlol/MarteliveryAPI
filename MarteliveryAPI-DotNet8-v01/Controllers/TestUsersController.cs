using MarteliveryAPI_DotNet8_v01.Data;
using MarteliveryAPI_DotNet8_v01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI_DotNet8_v01.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestUsersController : Controller
    {
        private readonly DataContext _context;

        public TestUsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet ("Users")]
        public async Task<ActionResult<List<TestUser>>> GetTestUsers()
        {
            var testUsers = await _context.TestUsers.ToListAsync();

            if (testUsers == null)
                return NotFound("Users not found");

            return Ok(testUsers);
        }

        [HttpGet("User/{id}")]
        public async Task<ActionResult<TestUser>> GetTestUser(string id)
        {
            var testUser = await _context.TestUsers.FindAsync(id);

            if (testUser == null)
                return NotFound("User not found");

            return Ok(testUser);
        }

        [HttpPost ("Register")]
        public async Task<ActionResult<TestUser>> Register(TestUser testUser)
        {
            var findTestUser = await _context.TestUsers.FirstOrDefaultAsync(x => x.Email.ToLower() == testUser.Email.ToLower());
            if (findTestUser == null)
            {
                _context.TestUsers.Add(new TestUser()
                {
                    Email = testUser.Email,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(testUser.Password)
                });
                await _context.SaveChangesAsync();
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("User already exists");
            }
        }
    }
}
