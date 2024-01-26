using AutoMapper;
using MarteliveryAPI.Data;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.Customer;
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
    public class CarrierRatingController(DataContext context, IMapper mapper) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        /*---------*/
        /*  ADMIN  */
        /*---------*/

        //Get method for admin to get all carrier ratings info with Mapped DTO
        [HttpGet("AdminGetAllCarrierRatingsInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetAllCarrierRatingsInfo()
        {
            var carrierRatings = await _context.CarrierRatings.ToListAsync();

            if (carrierRatings.Count == 0)
                return NotFound("Carrier ratings not found");

            var carrierRatingsDTO = _mapper.Map<List<GetCarrierRatingInfoDTO>>(carrierRatings);

            return Ok(carrierRatingsDTO);
        }

        //Get method for admin to get carrier rating info by id with Mapped DTO
        [HttpGet("AdminGetCarrierRatingInfo/{carrierRatingId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetCarrierRatingInfo(string carrierRatingId)
        {
            var carrierRating = await _context.CarrierRatings.FindAsync(carrierRatingId);

            if (carrierRating == null)
                return NotFound("Carrier rating not found");

            var carrierRatingDTO = _mapper.Map<GetCarrierRatingInfoDTO>(carrierRating);

            return Ok(carrierRatingDTO);
        }

        //Post method for admin to create a carrier rating with Mapped DTO
        [HttpPost("AdminCreateCarrierRating")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCreateCarrierRating(AdminCarrierRatingDTO carrierRatingDTO)
        {
            if (carrierRatingDTO == null)
                return BadRequest("Model is empty");
            
            var carrierRating = _mapper.Map<CarrierRating>(carrierRatingDTO);

            await _context.CarrierRatings.AddAsync(carrierRating);
            await _context.SaveChangesAsync();

            return Ok("Carrier rating created successfully");
        }

        //Put method for admin to update a carrier rating by id with Mapped DTO
        [HttpPut("AdminUpdateCarrierRating/{carrierRatingId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateCarrierRating(string carrierRatingId, AdminCarrierRatingDTO carrierRatingDTO)
        {
            if (carrierRatingDTO == null)
                return BadRequest("Model is empty");

            var carrierRating = await _context.CarrierRatings.FindAsync(carrierRatingId);

            if (carrierRating == null)
                return NotFound("Carrier rating not found");

            _mapper.Map(carrierRatingDTO, carrierRating);

            _context.CarrierRatings.Update(carrierRating);
            await _context.SaveChangesAsync();

            return Ok("Carrier rating updated successfully");
        }

        //Delete method for admin to delete a carrier rating by id
        [HttpDelete("AdminDeleteCarrierRating/{carrierRatingId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDeleteCarrierRating(string carrierRatingId)
        {
            var carrierRating = await _context.CarrierRatings.FindAsync(carrierRatingId);

            if (carrierRating == null)
                return NotFound("Carrier rating not found");

            _context.CarrierRatings.Remove(carrierRating);
            await _context.SaveChangesAsync();

            return Ok("Carrier rating deleted");
        }

        /*-----------*/
        /*  CARRIER  */
        /*-----------*/

        //Get method for carrier to get all carrier ratings info with Mapped DTO
        [HttpGet("CarrierGetAllCarrierRatingsInfo")]
        [Authorize(Roles = "Carrier")]
        public async Task<IActionResult> CarrierGetAllCarrierRatingsInfo()
        {
            var carrierRatings = await _context.CarrierRatings.Where(cr => cr.Delivery.Quote.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();

            if (carrierRatings.Count == 0)
                return NotFound("Carrier ratings not found");

            var carrierRatingsDTO = _mapper.Map<List<GetCarrierRatingInfoDTO>>(carrierRatings);

            return Ok(carrierRatingsDTO);
        }

        /*------------*/
        /*  CUSTOMER  */
        /*------------*/

        //Get method for customer to get all carrier ratings info he created with Mapped DTO
        [HttpGet("CustomerGetAllCarrierRatingsInfo")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerGetAllCarrierRatingsInfo()
        {
            var carrierRatings = await _context.CarrierRatings.Where(cr => cr.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();

            if (carrierRatings.Count == 0)
                return NotFound("Carrier ratings not found");

            var carrierRatingsDTO = _mapper.Map<List<GetCarrierRatingInfoDTO>>(carrierRatings);

            return Ok(carrierRatingsDTO);
        }

        //Post method for customer to create a carrier rating with Mapped DTO
        [HttpPost("CustomerCreateCarrierRating")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerCreateCarrierRating(CustomerCarrierRatingDTO carrierRatingDTO)
        {
            if (carrierRatingDTO == null)
                return BadRequest("Model is empty");

            var carrierRating = _mapper.Map<CarrierRating>(carrierRatingDTO);

            carrierRating.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Check if delivery exists
            var delivery = await _context.Deliveries.Where(d => d.DeliveryId == carrierRating.DeliveryId).FirstOrDefaultAsync();
            if (delivery == null)
                return NotFound("Delivery not found");

            //Check if delivery is linked to the quote linked to the parcel of the customer
            var quote = await _context.Quotes.Where(q => q.QuoteId == delivery.QuoteId).FirstOrDefaultAsync();
            var parcel = await _context.Parcels.Where(p => p.ParcelId == quote.ParcelId).FirstOrDefaultAsync();
            var customer = await _context.Users.Where(u => u.Id == parcel.UserId).FirstOrDefaultAsync();
            if (customer.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return BadRequest("Delivery is not linked to the quote linked to the parcel of the customer");

            //Check if delivery status is delivered
            if (delivery.DeliveryStatus != "Delivered")
                return BadRequest("Delivery can be rated only if it is \"Delivered\"");

            //Check if carrier rating already exists
            if (await _context.CarrierRatings.AnyAsync(cr => cr.DeliveryId == carrierRating.DeliveryId))
                return BadRequest("Carrier rating already exists for this delivery");

            //Check if carrier rating is between 1 and 5
            if (carrierRating.CarrierRate < 1 || carrierRating.CarrierRate > 5)
                return BadRequest("Carrier rating must be between 1 and 5");

            await _context.CarrierRatings.AddAsync(carrierRating);
            await _context.SaveChangesAsync();

            return Ok("Carrier rating created successfully");
        }

        //Put method for customer to update a carrier rating by id with Mapped DTO
        [HttpPut("CustomerUpdateCarrierRating/{carrierRatingId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerUpdateCarrierRating(string carrierRatingId, CustomerCarrierRatingDTO carrierRatingDTO)
        {
            if (carrierRatingDTO == null)
                return BadRequest("Model is empty");

            var carrierRating = await _context.CarrierRatings.FindAsync(carrierRatingId);

            if (carrierRating == null)
                return NotFound("Carrier rating not found");

            //Check if delivery exists
            var delivery = await _context.Deliveries.Where(d => d.DeliveryId == carrierRating.DeliveryId).FirstOrDefaultAsync();
            if (delivery == null)
                return NotFound("Delivery not found");

            //Check if delivery is linked to the quote linked to the parcel of the customer
            var quote = await _context.Quotes.Where(q => q.QuoteId == delivery.QuoteId).FirstOrDefaultAsync();
            var parcel = await _context.Parcels.Where(p => p.ParcelId == quote.ParcelId).FirstOrDefaultAsync();
            var customer = await _context.Users.Where(u => u.Id == parcel.UserId).FirstOrDefaultAsync();
            if (customer.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return BadRequest("Delivery is not linked to the quote linked to the parcel of the customer");

            //Check if delivery status is delivered
            if (delivery.DeliveryStatus != "Delivered")
                return BadRequest("Delivery can be rated only if it is \"Delivered\"");

            //Check if carrier rating is between 1 and 5
            if (carrierRatingDTO.CarrierRate < 1 || carrierRatingDTO.CarrierRate > 5)
                return BadRequest("Carrier rating must be between 1 and 5");

            //Check if the delivery is the same
            if (carrierRating.DeliveryId != carrierRatingDTO.DeliveryId)
                return BadRequest("Delivery cannot be changed");

            _mapper.Map(carrierRatingDTO, carrierRating);

            _context.CarrierRatings.Update(carrierRating);
            await _context.SaveChangesAsync();

            return Ok("Carrier rating updated successfully");
        }

        //Delete method for customer to delete a carrier rating by id
        [HttpDelete("CustomerDeleteCarrierRating/{carrierRatingId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerDeleteCarrierRating(string carrierRatingId)
        {
            var carrierRating = await _context.CarrierRatings.FindAsync(carrierRatingId);

            if (carrierRating == null)
                return NotFound("Carrier rating not found");

            //Check if delivery exists
            var delivery = await _context.Deliveries.Where(d => d.DeliveryId == carrierRating.DeliveryId).FirstOrDefaultAsync();
            if (delivery == null)
                return NotFound("Delivery not found");

            //Check if delivery is linked to the quote linked to the parcel of the customer
            var quote = await _context.Quotes.Where(q => q.QuoteId == delivery.QuoteId).FirstOrDefaultAsync();
            var parcel = await _context.Parcels.Where(p => p.ParcelId == quote.ParcelId).FirstOrDefaultAsync();
            var customer = await _context.Users.Where(u => u.Id == parcel.UserId).FirstOrDefaultAsync();
            if (customer.Id != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return BadRequest("Delivery is not linked to the quote linked to the parcel of the customer");

            _context.CarrierRatings.Remove(carrierRating);
            await _context.SaveChangesAsync();

            return Ok("Carrier rating deleted");
        }
    }
}
