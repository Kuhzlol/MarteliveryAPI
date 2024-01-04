using MarteliveryAPI_DotNet8_v01.Data;
using MarteliveryAPI_DotNet8_v01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI_DotNet8_v01.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrierRatingController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet ("GetCarrierRatingsInfo")]
        public async Task<ActionResult<List<CarrierRating>>> GetCarrierRatingsInfo()
        {
            var carrierRatings = await _context.CarrierRatings.ToListAsync();

            if (carrierRatings.Count == 0)
                return NotFound("Carrier ratings not found");
            
            return Ok(carrierRatings);
        }

        [HttpGet("GetCarrierRating/{id1, id2}")]
        public async Task<ActionResult<CarrierRating>> GetCarrierRatingInfo(string id1, string id2)
        {
            var carrierRating = await _context.CarrierRatings.FindAsync(id1, id2);

            if (carrierRating == null)
                return NotFound("Carrier rating not found");

            return Ok(carrierRating);
        }

        [HttpPost ("CreateCarrierRating")]
        public async Task<ActionResult<CarrierRating>> CreateCarrierRating(CarrierRating carrierRating)
        {
            _context.CarrierRatings.Add(carrierRating);
            await _context.SaveChangesAsync();

            return Ok("Carrier rating created");
        }

        [HttpPut("UpdateCarrierRating/{id1, id2}")]
        public async Task<ActionResult> UpdateCarrierRating(string id1, string id2, CarrierRating carrierRating)
        {
            var carrierRatingToUpdate = await _context.CarrierRatings.FindAsync(id1, id2);

            if (carrierRatingToUpdate == null)
                return NotFound("Carrier rating not found");

            bool isUpdated = false;
            if (carrierRatingToUpdate.CarrierRate != carrierRating.CarrierRate)
            {
                carrierRatingToUpdate.CarrierRate = carrierRating.CarrierRate;
                isUpdated = true;
            }
            if (isUpdated)
            {
                _context.CarrierRatings.Update(carrierRatingToUpdate);
                await _context.SaveChangesAsync();
            }

            return Ok("Carrier rating updated");
        }

        [HttpDelete("DeleteCarrierRating/{id1, id2}")]
        public async Task<ActionResult> DeleteCarrierRating(string id1, string id2)
        {
            var carrierRatingToDelete = await _context.CarrierRatings.FindAsync(id1, id2);

            if (carrierRatingToDelete == null)
                return NotFound("Carrier rating not found");

            _context.CarrierRatings.Remove(carrierRatingToDelete);
            await _context.SaveChangesAsync();

            return Ok("Carrier rating deleted");
        }
    }
}
