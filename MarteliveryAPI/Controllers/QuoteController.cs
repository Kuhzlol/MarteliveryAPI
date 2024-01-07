using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarteliveryAPI.Data;
using MarteliveryAPI.Models;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuoteController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet ("GetQuotesInfo")]
        public async Task<ActionResult<List<Quote>>> GetQuotesInfo()
        {
            var quotes = await _context.Quotes.ToListAsync();

            if (quotes.Count == 0)
                return NotFound("Quotes not found");
            
            return Ok(quotes);
        }

        [HttpGet("GetQuote/{id}")]
        public async Task<ActionResult<Quote>> GetQuoteInfo(string id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
                return NotFound("Quote not found");

            return Ok(quote);
        }

        [HttpPost ("CreateQuote")]
        public async Task<ActionResult<Quote>> CreateQuote(Quote quote)
        {            
            _context.Quotes.Add(new Quote()
            {
                PricePerKm = quote.PricePerKm,
                TotalPrice = quote.TotalPrice,
                CarrierId = quote.CarrierId,
                ParcelId = quote.ParcelId
            });
            await _context.SaveChangesAsync();

            return Ok("Quote created");
        }

        [HttpPut("UpdateQuote/{id}")]
        public async Task<ActionResult> UpdateQuote(string id, Quote quote)
        {
            var quoteToUpdate = await _context.Quotes.FindAsync(id);

            if (quoteToUpdate == null)
                return NotFound("Quote not found");

            bool isUpdated = false;
            if (quoteToUpdate.PricePerKm != quote.PricePerKm)
            {
                quoteToUpdate.PricePerKm = quote.PricePerKm;
                isUpdated = true;
            }
            if (quoteToUpdate.TotalPrice != quote.TotalPrice)
            {
                quoteToUpdate.TotalPrice = quote.TotalPrice;
                isUpdated = true;
            }
            if (quoteToUpdate.Status != quote.Status)
            {
                quoteToUpdate.Status = quote.Status;
                isUpdated = true;
            }

            if (isUpdated)
            {
                await _context.SaveChangesAsync();
                return Ok("Quote updated");
            }
            else
            {
                return Ok("No changes were made to the quote");
            }
        }

        [HttpDelete("DeleteQuote{id}")]
        public async Task<IActionResult> DeleteQuote(string id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
                return NotFound("Quote not found");

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return Ok("Quote deleted");
        }
    }
}
