using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarteliveryAPI.Data;
using MarteliveryAPI.Models.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.DTOs.Carrier;
using System.Security.Claims;
using MarteliveryAPI.Models.DTOs.User;
using MarteliveryAPI.Models.DTOs.Customer;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class QuoteController(DataContext context, IMapper mapper) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        /*---------*/
        /*  ADMIN  */
        /*---------*/

        //Get method for admin to get all quotes info with Mapped DTO
        [HttpGet ("AdminGetAllQuotesInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetAllQuotesInfo()
        {
            var quotes = await _context.Quotes.ToListAsync();

            if (quotes.Count == 0)
                return NotFound("Quotes not found");

            var quotesDTO = _mapper.Map<List<AdminQuoteInfoDTO>>(quotes);

            return Ok(quotesDTO);
        }

        //Get method for admin to get quote info by id with Mapped DTO
        [HttpGet ("AdminGetQuoteInfo/{quoteId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetQuoteInfo(string quoteId)
        {
            var quote = await _context.Quotes.FindAsync(quoteId);

            if (quote == null)
                return NotFound("Quote not found");

            var quoteDTO = _mapper.Map<AdminQuoteInfoDTO>(quote);

            return Ok(quoteDTO);
        }

        //Post method for admin to create a quote with Mapped DTO
        [HttpPost("AdminCreateQuote")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCreateQuote(AdminCreateQuoteDTO quoteDTO)
        {
            if (quoteDTO == null)
                return BadRequest("Quote data is empty");

            //Check if quote status is valid
            if (quoteDTO.Status != "Pending" || quoteDTO.Status != "Accepted" || quoteDTO.Status != "Rejected")
                return BadRequest("Quote status is invalid");

            //Calculate total price based on price per km and total distance from the parcel
            var parcel = await _context.Parcels.FindAsync(quoteDTO.ParcelId);
            if (parcel == null)
                return NotFound("Parcel not found");

            quoteDTO.TotalPrice = quoteDTO.PricePerKm * parcel.TotalDistance;

            var quote = _mapper.Map<Quote>(quoteDTO);

            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return Ok("Quote created");
        }

        //Put method for admin to update a quote by id with Mapped DTO
        [HttpPut("AdminUpdateQuote/{quoteId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateQuote(string quoteId, AdminUpdateQuoteDTO quoteDTO)
        {
            if (quoteDTO == null)
                return BadRequest("Quote data is empty");

            var quote = await _context.Quotes.FindAsync(quoteId);

            //Check if quote exists
            if (quote == null)
                return NotFound("Quote not found");

            //Check if quote status is valid
            if (quoteDTO.Status != "Pending" || quoteDTO.Status != "Accepted" || quoteDTO.Status != "Rejected")
                return BadRequest("Quote status is invalid");            

            //Calculate total price based on price per km and total distance from the parcel
            var parcel = await _context.Parcels.FindAsync(quoteDTO.ParcelId);
            if (parcel == null)
                return NotFound("Parcel not found");

            quoteDTO.TotalPrice = quoteDTO.PricePerKm * parcel.TotalDistance;

            quote = _mapper.Map(quoteDTO, quote);

            _context.Quotes.Update(quote);
            await _context.SaveChangesAsync();

            return Ok("Quote updated");
        }

        //Delete method for admin to delete a quote by id
        [HttpDelete("AdminDeleteQuote/{quoteId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDeleteQuote(string quoteId)
        {
            var quote = await _context.Quotes.FindAsync(quoteId);

            if (quote == null)
                return NotFound("Quote not found");

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return Ok("Quote deleted");
        }

        /*-----------*/
        /*  CARRIER  */
        /*-----------*/

        //Get method for carrier to get all quotes info with Mapped DTO
        [HttpGet ("GetMyCarrierQuotesInfo")]
        [Authorize(Roles = "Carrier")]
        public async Task<IActionResult> GetMyCarrierQuotesInfo()
        {
            var quotes = await _context.Quotes.Where(q => q.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();

            if (quotes.Count == 0)
                return NotFound("Quotes not found");

            var quotesDTO = _mapper.Map<List<QuoteInfoDTO>>(quotes);

            return Ok(quotesDTO);
        }

        //Post method for carrier to create a quote with Mapped DTO
        [HttpPost("CarrierCreateQuote")]
        [Authorize(Roles = "Carrier")]
        public async Task<IActionResult> CarrierCreateQuote(CarrierQuoteDTO quoteDTO)
        {
            var quote = _mapper.Map<Quote>(quoteDTO);

            quote.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Check if quote already exists for the parcel from the carrier
            var quoteExists = await _context.Quotes.Where(q => q.ParcelId == quoteDTO.ParcelId && q.UserId == quote.UserId).FirstOrDefaultAsync();
            if (quoteExists != null)
                return BadRequest("The carrier has already created a quote for this parcel");

            var parcel = await _context.Parcels.FindAsync(quoteDTO.ParcelId);
            if (parcel == null)
                return NotFound("Parcel not found");

            //Check if a quote has already been accepted for the parcel
            var acceptedQuote = await _context.Quotes.Where(q => q.ParcelId == quoteDTO.ParcelId && q.Status == "Accepted").FirstOrDefaultAsync();
            if (acceptedQuote != null)
                return BadRequest("A quote has already been accepted for this parcel");

            //Calculate total price based on price per km and total distance from the parcel
            quote.TotalPrice = quoteDTO.PricePerKm * parcel.TotalDistance;

            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return Ok("Quote created");
        }

        //Put method for carrier to update a quote by id with Mapped DTO
        [HttpPut("CarrierUpdateQuote/{quoteId}")]
        [Authorize(Roles = "Carrier")]
        public async Task<IActionResult> CarrierUpdateQuote(string quoteId, CarrierQuoteDTO quoteDTO)
        {
            if (quoteDTO == null)
                return BadRequest("Model is empty");
            
            var quote = await _context.Quotes.FindAsync(quoteId);

            //Check if quote belongs to the carrier
            if (quote.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return BadRequest("Quote doesn't belong to the carrier");

            //Check if quote exists
            if (quote == null)
                return NotFound("Quote not found");

            //Check if quote status is different from pending
            if (quote.Status != "Pending")
                return BadRequest("Quote with a status different from \"Pending\" can't be updated");

            //Check if parcelid is different from the one in the quote
            if (quote.ParcelId != quoteDTO.ParcelId)
                return BadRequest("ParcelId is different from the one in the quote");

            //Calculate total price based on price per km and total distance from the parcel
            var parcel = await _context.Parcels.FindAsync(quoteDTO.ParcelId);
            if (parcel == null)
                return NotFound("Parcel not found");

            quote = _mapper.Map(quoteDTO, quote);

            quote.TotalPrice = quoteDTO.PricePerKm * parcel.TotalDistance;

            _context.Quotes.Update(quote);
            await _context.SaveChangesAsync();

            return Ok("Quote updated");
        }

        //Delete method for carrier to delete a quote by id
        [HttpDelete("CarrierDeleteQuote/{quoteId}")]
        [Authorize(Roles = "Carrier")]
        public async Task<IActionResult> CarrierDeleteQuote(string quoteId)
        {
            var quote = await _context.Quotes.FindAsync(quoteId);

            //Check if quote belongs to the carrier
            if (quote.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return BadRequest("Quote doesn't belong to the carrier");

            if (quote == null)
                return NotFound("Quote not found");

            //Check if quote status is different from pending
            if (quote.Status != "Pending")
                return BadRequest("Quote with a status different from \"Pending\" can't be deleted");

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return Ok("Quote deleted");
        }

        /*------------*/
        /*  CUSTOMER  */
        /*------------*/

        //Get method for customer to get all quotes linked to their parcels with Mapped DTO
        [HttpGet("GetMyCustomerQuotesInfo")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyCustomerQuotesInfo()
        {
            var quotes = await _context.Quotes.Where(q => q.Parcel.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();

            if (quotes.Count == 0)
                return NotFound("Quotes not found");

            var quotesDTO = _mapper.Map<List<QuoteInfoDTO>>(quotes);

            return Ok(quotesDTO);
        }

        //Put method for customer to accept a quote linked to his parcel by id with Mapped DTO
        [HttpPut("CustomerAcceptQuote/{quoteId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerAcceptQuote(string quoteId, CustomerAcceptQuoteDTO acceptQuoteDTO)
        {
            var quote = await _context.Quotes.FindAsync(quoteId);

            //Check if quote exists
            if (quote == null)
                return NotFound("Quote not found");

            //Check if Customer is the owner of the parcel linked to the quote that he wants to accept
            var parcel = await _context.Parcels.FindAsync(quote.ParcelId);
            if (parcel.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Unauthorized("You are not the owner of this parcel");

            //Check if quote status is different from pending
            if (quote.Status == "Pending")
                acceptQuoteDTO.Status = "Accepted";
            else
                return BadRequest("A quote has already been accepted for this parcel");

            //Set all other quotes linked to the parcel to "Rejected"
            var quotes = await _context.Quotes.Where(q => q.ParcelId == quote.ParcelId).ToListAsync();
            foreach (var q in quotes)
            {
                if (q.QuoteId != quote.QuoteId)
                    q.Status = "Rejected";
            }

            quote = _mapper.Map(acceptQuoteDTO, quote);

            _context.Quotes.Update(quote);
            await _context.SaveChangesAsync();

            return Ok("Quote accepted");
        }
    }
}
