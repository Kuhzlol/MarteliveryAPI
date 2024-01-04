using MarteliveryAPI_DotNet8_v01.Data;
using MarteliveryAPI_DotNet8_v01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI_DotNet8_v01.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrierController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet ("GetCarriersInfo")]
        public async Task<ActionResult<List<Carrier>>> GetCarriersInfo()
        {
            var carriers = await _context.Carriers.ToListAsync();

            if (carriers.Count == 0)
                return NotFound("Carriers not found");
            
            return Ok(carriers);
        }

        [HttpGet ("GetCarrierInfo/{id}")]
        public async Task<ActionResult<Carrier>> GetCarrierInfo(string id)
        {
            var carrier = await _context.Carriers.FindAsync(id);

            if (carrier == null)
                return NotFound("Carrier not found");

            return Ok(carrier);
        }

        [HttpPost ("CreateCarrier")]
        public async Task<ActionResult<Carrier>> CreateCarrier(Carrier carrier)
        {
            _context.Carriers.Add(carrier);
            await _context.SaveChangesAsync();

            return Ok("Carrier created");
        }

        [HttpPut ("UpdateCarrier/{id}")]
        public async Task<ActionResult<Carrier>> UpdateCarrier(Carrier carrier)
        {
            var carrierToUpdate = await _context.Carriers.FindAsync(carrier.Id);

            if (carrierToUpdate == null)
                return NotFound("Carrier not found");

            bool isUpdated = false;
            if (carrierToUpdate.FirstName != carrier.FirstName)
            {
                carrierToUpdate.FirstName = carrier.FirstName;
                isUpdated = true;
            }
            if (carrierToUpdate.LastName != carrier.LastName)
            {
                carrierToUpdate.LastName = carrier.LastName;
                isUpdated = true;
            }
            if (carrierToUpdate.Email != carrier.Email)
            {
                carrierToUpdate.Email = carrier.Email;
                isUpdated = true;
            }
            if (carrierToUpdate.PhoneNumber != carrier.PhoneNumber)
            {
                carrierToUpdate.PhoneNumber = carrier.PhoneNumber;
                isUpdated = true;
            }

            if (isUpdated)
            {
                await _context.SaveChangesAsync();
                return Ok("Carrier updated");
            }
            else
            {
                return Ok("No changes were made to the carrier");
            }
        }

        [HttpDelete ("DeleteCarrier/{id}")]
        public async Task<ActionResult<Carrier>> DeleteCarrier(string id)
        {
            var carrier = await _context.Carriers.FindAsync(id);
            if (carrier == null)
                return NotFound("Carrier not found");

            _context.Carriers.Remove(carrier);
            await _context.SaveChangesAsync();

            return Ok("Carrier deleted");
        }
    }
}
