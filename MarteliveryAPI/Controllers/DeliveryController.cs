using AutoMapper;
using MarteliveryAPI.Data;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.Carrier;
using MarteliveryAPI.Models.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DeliveryController(DataContext context, IMapper mapper) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        /*---------*/
        /*  ADMIN  */
        /*---------*/

        //Get method for admin to get all deliveries info with Mapped DTO
        [HttpGet("AdminGetAllDeliveriesInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetAllDeliveriesInfo()
        {
            var deliveries = await _context.Deliveries.ToListAsync();

            if (deliveries.Count == 0)
                return NotFound("Deliveries not found");

            var deliveriesDTO = _mapper.Map<List<GetDeliveryInfoDTO>>(deliveries);

            return Ok(deliveriesDTO);
        }

        //Get method for admin to get delivery info by id with Mapped DTO
        [HttpGet("AdminGetDeliveryInfo/{deliveryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetDeliveryInfo(string deliveryId)
        {
            var delivery = await _context.Deliveries.FindAsync(deliveryId);

            if (delivery == null)
                return NotFound("Delivery not found");

            var deliveryDTO = _mapper.Map<GetDeliveryInfoDTO>(delivery);

            return Ok(deliveryDTO);
        }

        //Post method for admin to create a delivery with Mapped DTO
        [HttpPost("AdminCreateDelivery")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCreateDelivery(AdminCreateDeliveryDTO deliveryDTO)
        {
            if (deliveryDTO == null)
                return BadRequest("Model is empty");
            
            var delivery = _mapper.Map<Delivery>(deliveryDTO);

            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            return Ok("Delivery created successfully");
        }

        //Put method for admin to update a delivery by id with Mapped DTO
        [HttpPut("AdminUpdateDelivery/{deliveryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateDelivery(string deliveryId, AdminUpdateDeliveryDTO deliveryDTO)
        {
            if (deliveryDTO == null)
                return BadRequest("Model is empty");

            var delivery = await _context.Deliveries.FindAsync(deliveryId);

            if (delivery == null)
                return NotFound("Delivery not found");

            _mapper.Map(deliveryDTO, delivery);

            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();

            return Ok("Delivery updated successfully");
        }

        //Delete method for admin to delete a delivery by id
        [HttpDelete("AdminDeleteDelivery/{deliveryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDeleteDelivery(string deliveryId)
        {
            var delivery = await _context.Deliveries.FindAsync(deliveryId);

            if (delivery == null)
                return NotFound("Delivery not found");

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return Ok("Delivery deleted");
        }

        /*-----------*/
        /*  CARRIER  */
        /*-----------*/

        //Get method for carrier to get all deliveries info with Mapped DTO
        [HttpGet("CarrierGetAllDeliveriesInfo")]
        [Authorize(Roles = "Carrier")]
        public async Task<IActionResult> CarrierGetAllDeliveriesInfo()
        {
            var deliveries = await _context.Deliveries.Where(d => d.Quote.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();

            if (deliveries.Count == 0)
                return NotFound("Deliveries not found");

            var deliveriesDTO = _mapper.Map<List<GetDeliveryInfoDTO>>(deliveries);

            return Ok(deliveriesDTO);
        }

        //Post method for carrier to create a delivery with Mapped DTO
        [HttpPost("CarrierCreateDelivery")]
        [Authorize(Roles = "Carrier")]
        public async Task<IActionResult> CarrierCreateDelivery(CarrierDeliveryDTO deliveryDTO)
        {
            if (deliveryDTO == null)
                return BadRequest("Model is empty");

            var delivery = _mapper.Map<Delivery>(deliveryDTO);

            //Check if quote exists
            var quote = await _context.Quotes.Where(q => q.QuoteId == delivery.QuoteId).FirstOrDefaultAsync();
            if (quote == null)
                return NotFound("Quote not found");

            //Check if carrier is the owner of the quote linked to the delivery he wants to create
            if (quote.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return BadRequest("You are not the owner of this quote");

            //Check if quote status is accepted
            if (quote.Status != "Accepted")
                return BadRequest("Delivery can be made only on \"Accepted\" quote");

            //Check if a delivery has already been made on this quote
            if (await _context.Deliveries.AnyAsync(d => d.QuoteId == delivery.QuoteId))
                return BadRequest("Delivery has already been made on this quote");

            //Set the delivery status to "Collected by the carrier"
            delivery.DeliveryStatus = "Collected by the carrier";

            //Set the delivery time to the current UTC time + 2 hours
            delivery.DeliveryTime = DateTime.UtcNow.AddHours(2);

            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            return Ok("Delivery created successfully");
        }

        //Put method for carrier to update a delivery by id with Mapped DTO
        [HttpPut("CarrierUpdateDelivery/{deliveryId}")]
        [Authorize(Roles = "Carrier")]
        public async Task<IActionResult> CarrierUpdateDelivery(string deliveryId, CarrierDeliveryDTO deliveryDTO)
        {
            if (deliveryDTO == null)
                return BadRequest("Model is empty");

            var delivery = await _context.Deliveries.FindAsync(deliveryId);

            if (delivery == null)
                return NotFound("Delivery not found");

            //Check if carrier is the owner of the delivery he wants to update
            var quote = await _context.Quotes.Where(q => q.QuoteId == delivery.QuoteId).FirstOrDefaultAsync();
            if (quote.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return BadRequest("You are not the owner of this delivery");

            //Check if quote is still the same
            if (delivery.QuoteId != deliveryDTO.QuoteId)
                return BadRequest("Quote must be the same");

            //Check if delivery time is lower than Pickup time
            if (deliveryDTO.DeliveryTime < delivery.PickupTime)
                return BadRequest("Delivery time can't be lower than Pickup time");

            //Check if delivery status is "Collected by the carrier" or "On delivery" to update it
            if (delivery.DeliveryStatus == "Collected by the carrier")
            { 
                deliveryDTO.DeliveryStatus = "On delivery";
                deliveryDTO.DeliveryTime = (DateTime)delivery.DeliveryTime;
            }
            else if (delivery.DeliveryStatus == "On delivery")
            {
                deliveryDTO.DeliveryStatus = "Delivered";
                deliveryDTO.DeliveryTime = DateTime.UtcNow;
            }
            else
            {
                return BadRequest("Delivery status can be updated only to \"Collected by the carrier\" or \"On delivery\"");
            }

            _mapper.Map(deliveryDTO, delivery);

            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();

            return Ok("Delivery updated successfully");
        }

        /*------------*/
        /*  CUSTOMER  */
        /*------------*/

        //Get method for customer to get all deliveries linked to the quotes he accepted with Mapped DTO
        [HttpGet("CustomerGetAllDeliveriesInfo")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerGetAllDeliveriesInfo()
        {
            var deliveries = await _context.Deliveries.Where(d => d.Quote.Parcel.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();

            if (deliveries.Count == 0)
                return NotFound("Deliveries not found");

            var deliveriesDTO = _mapper.Map<List<GetDeliveryInfoDTO>>(deliveries);

            return Ok(deliveriesDTO);
        }
    }
}
