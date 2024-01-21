using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarteliveryAPI.Data;
using MarteliveryAPI.Models.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MarteliveryAPI.Models.DTOs.Admin;

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
        [HttpGet ("AdminGetQuoteInfo/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetQuoteInfo(string id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
                return NotFound("Quote not found");

            var quoteDTO = _mapper.Map<AdminQuoteInfoDTO>(quote);

            return Ok(quoteDTO);
        }

        //Post method for admin to create a quote with Mapped DTO
        [HttpPost("AdminCreateQuote")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCreateQuote(AdminQuoteCreateDTO quoteDTO)
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
        [HttpPut("AdminUpdateQuote/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateQuote(string id, AdminQuoteUpdateDTO quoteDTO)
        {
            if (quoteDTO == null)
                return BadRequest("Quote data is empty");

            var quote = await _context.Quotes.FindAsync(id);

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
        [HttpDelete("AdminDeleteQuote/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDeleteQuote(string id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
                return NotFound("Quote not found");

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return Ok("Quote deleted");
        }

        /*---------*/
        /*  USERS  */
        /*---------*/

        //Get method for user to get all quotes info with Mapped DTO
        [HttpGet ("UserGetAllQuotesInfo")]
    }
}
