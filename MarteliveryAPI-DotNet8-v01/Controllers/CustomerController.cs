using MarteliveryAPI_DotNet8_v01.Data;
using MarteliveryAPI_DotNet8_v01.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI_DotNet8_v01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet (Name = "GetAllCustomers")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            if (customers == null)
                return NotFound("Customers not found");

            return Ok(customers);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);
        }

        [HttpPost (Name = "AddCustomer")]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok("Customer added");
        }

        [HttpPut("{id}", Name = "UpdateCustomer")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
        {
            var customerToUpdate = await _context.Customers.FindAsync(id);

            if (customerToUpdate == null)
                return NotFound("Customer not found");

            customerToUpdate.firstname = customer.firstname;
            customerToUpdate.lastname = customer.lastname;
            customerToUpdate.email = customer.email;
            customerToUpdate.mobile = customer.mobile;

            await _context.SaveChangesAsync();

            return Ok("Customer updated");
        }

        [HttpDelete("{id}", Name = "DeleteCustomer")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
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
