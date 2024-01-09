using MarteliveryAPI.Data;
using MarteliveryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet ("GetCustomersInfo")]
        public async Task<ActionResult<List<Customer>>> GetCustomersInfo()
        {
            var customers = await _context.Customers.ToListAsync();

            if (customers.Count == 0)
                return NotFound("Users not found");

            return Ok(customers);
        }

        [HttpGet("GetCustomerInfo/{id}")]
        public async Task<IActionResult> GetCustomerInfo(string id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound("User not found");

            return Ok(customer);
        }

        [HttpPost ("Register")]
        public async Task<IActionResult> Register(Customer customer)
        {
            var findCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Email.ToLower() == customer.Email.ToLower());
            
            if (findCustomer == null)
            {
                // Verify that the date of birth is not in the future and that the user is at least 18 years old
                if (customer.DateOfBirth > DateOnly.FromDateTime(DateTime.Now) || customer.DateOfBirth.AddYears(18) > DateOnly.FromDateTime(DateTime.Now))
                    return BadRequest("Invalid date of birth");

                _context.Customers.Add(new Customer()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(customer.Password),
                    DateOfBirth = customer.DateOfBirth
                });
                await _context.SaveChangesAsync();

                return Ok("User registered");
            }
            else
            {
                return BadRequest("Email already exists");
            }
        }

        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, Customer customer)
        {
            var customerToUpdate = await _context.Customers.FindAsync(id);

            if (customerToUpdate == null)
                return NotFound("Customer not found");

            // Compare updated customer data with original customer data
            bool isUpdated = false;
            if (customerToUpdate.FirstName != customer.FirstName)
            {
                isUpdated = true;
                customerToUpdate.FirstName = customer.FirstName;
            }
            if (customerToUpdate.LastName != customer.LastName)
            {
                isUpdated = true;
                customerToUpdate.LastName = customer.LastName;
            }
            if (customerToUpdate.Email != customer.Email)
            {
                isUpdated = true;
                customerToUpdate.Email = customer.Email;
            }
            if (customerToUpdate.PhoneNumber != customer.PhoneNumber)
            {
                isUpdated = true;
                customerToUpdate.PhoneNumber = customer.PhoneNumber;
            }

            if (isUpdated)
            {
                await _context.SaveChangesAsync();
                return Ok("Customer updated");
            }
            else
            {
                return Ok("No changes were made to the customer");
            }
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customerToDelete = await _context.Customers.FindAsync(id);

            if (customerToDelete == null)
                return NotFound("Customer not found");

            _context.Customers.Remove(customerToDelete);
            await _context.SaveChangesAsync();

            return Ok("Customer deleted");
        }
    }
}
