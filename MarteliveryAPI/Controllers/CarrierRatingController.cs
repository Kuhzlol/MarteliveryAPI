using MarteliveryAPI.Data;
using MarteliveryAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI.Controllers
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

        [HttpGet("GetCarrierRating/{id}")]
        public async Task<ActionResult<CarrierRating>> GetCarrierRatingInfo(string id)
        {
            var carrierRating = await _context.CarrierRatings.FindAsync(id);

            if (carrierRating == null)
                return NotFound("Carrier rating not found");

            return Ok(carrierRating);
        }

        [HttpPost ("CreateCarrierRating")]
        public async Task<ActionResult<CarrierRating>> CreateCarrierRating(CarrierRating carrierRating)
        {
            _context.CarrierRatings.Add(new CarrierRating
            {
                UserId = carrierRating.UserId,
                DeliveryId = carrierRating.DeliveryId,
                CarrierRate = carrierRating.CarrierRate
            });
            await _context.SaveChangesAsync();

            return Ok("Carrier rating created");
        }

        [HttpPut("UpdateCarrierRating/{id}")]
        public async Task<ActionResult> UpdateCarrierRating(string id, CarrierRating carrierRating)
        {
            var carrierRatingToUpdate = await _context.CarrierRatings.FindAsync(id);

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

        [HttpDelete("DeleteCarrierRating/{id}")]
        public async Task<ActionResult> DeleteCarrierRating(string id)
        {
            var carrierRatingToDelete = await _context.CarrierRatings.FindAsync(id);

            if (carrierRatingToDelete == null)
                return NotFound("Carrier rating not found");

            _context.CarrierRatings.Remove(carrierRatingToDelete);
            await _context.SaveChangesAsync();

            return Ok("Carrier rating deleted");
        }
    }
}
