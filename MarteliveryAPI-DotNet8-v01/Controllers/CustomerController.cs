﻿using MarteliveryAPI_DotNet8_v01.Data;
using MarteliveryAPI_DotNet8_v01.Models;
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
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);
        }

        [HttpPost (Name = "AddCustomer")]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            // Do not aad customer id, it is generated by the database
            if (customer.CustomerId != Guid.Empty)
            {
                return BadRequest("ID should not be specified when adding a customer.");
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok("Customer added");
        }

        [HttpPut("{id}", Name = "UpdateCustomer")]
        public async Task<ActionResult<Customer>> UpdateCustomer(Guid id, Customer customer)
        {
            var customerToUpdate = await _context.Customers.FindAsync(id);

            if (customerToUpdate == null)
                return NotFound("Customer not found");

            // Compare updated customer data with original customer data
            bool isModified = false;
            if (customerToUpdate.FirstName != customer.FirstName)
            {
                isModified = true;
                customerToUpdate.FirstName = customer.FirstName;
            }
            if (customerToUpdate.LastName != customer.LastName)
            {
                isModified = true;
                customerToUpdate.LastName = customer.LastName;
            }
            if (customerToUpdate.Email != customer.Email)
            {
                isModified = true;
                customerToUpdate.Email = customer.Email;
            }
            if (customerToUpdate.PhoneNumber != customer.PhoneNumber)
            {
                isModified = true;
                customerToUpdate.PhoneNumber = customer.PhoneNumber;
            }

            if (isModified)
            {
                await _context.SaveChangesAsync();
                return Ok("Customer updated");
            }
            else
            {
                return Ok("No changes were made to the customer");
            }
        }

        [HttpDelete("{id}", Name = "DeleteCustomer")]
        public async Task<ActionResult<Customer>> DeleteCustomer(Guid id)
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
