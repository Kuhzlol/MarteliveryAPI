using MarteliveryAPI_DotNet8_v01.Data;
using MarteliveryAPI_DotNet8_v01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI_DotNet8_v01.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeliveryController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet ("GetDeliveriesInfo")]
        public async Task<ActionResult<List<Delivery>>> GetDeliveriesInfo()
        {
            var deliveries = await _context.Deliveries.ToListAsync();

            if (deliveries.Count == 0)
                return NotFound("Deliveries not found");
            
            return Ok(deliveries);
        }

        [HttpGet("GetDelivery/{id}")]
        public async Task<ActionResult<Delivery>> GetDeliveryInfo(string id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
                return NotFound("Delivery not found");

            return Ok(delivery);
        }

        [HttpPost ("CreateDelivery")]
        public async Task<ActionResult<Delivery>> CreateDelivery(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            return Ok("Delivery created");
        }

        [HttpPut ("UpdateDelivery/{id}")]
        public async Task<IActionResult> UpdateDelivery(Guid id, Delivery delivery)
        {
            var deliveryToUpdate = await _context.Deliveries.FindAsync(id);

            if (deliveryToUpdate == null)
                return NotFound("Delivery not found");

            bool isUpdated = false;
            if (deliveryToUpdate.DeliveryTime != delivery.DeliveryTime)
            {
                deliveryToUpdate.DeliveryTime = delivery.DeliveryTime;
                isUpdated = true;
            }
            if (deliveryToUpdate.DeliveryStatus != delivery.DeliveryStatus)
            {
                deliveryToUpdate.DeliveryStatus = delivery.DeliveryStatus;
                isUpdated = true;
            }

            if (isUpdated)
            {
                _context.Deliveries.Update(deliveryToUpdate);
                await _context.SaveChangesAsync();
                return Ok("Delivery updated");
            }
            else
            {
                return Ok("No changes were made to the delivery");
            }            
        }

        [HttpDelete ("DeleteDelivery/{id}")]
        public async Task<ActionResult> DeleteDelivery(string id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
                return NotFound("Delivery not found");

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return Ok("Delivery deleted");
        }
    }
}
