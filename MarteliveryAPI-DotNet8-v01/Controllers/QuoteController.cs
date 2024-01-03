using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarteliveryAPI_DotNet8_v01.Data;
using MarteliveryAPI_DotNet8_v01.Models;

namespace MarteliveryAPI_DotNet8_v01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly DataContext _context;

        public QuoteController(DataContext context)
        {
            _context = context;
        }

        [HttpGet ("GetQuotes")]
        public async Task<ActionResult<List<Quote>>> GetQuotes()
        {
            var quotes = await _context.Quotes.ToListAsync();

            if (quotes.Count == 0)
                return NotFound("Quotes not found");
            
            return await _context.Quotes.ToListAsync();
        }

        [HttpGet("GetQuote/{id}")]
        public async Task<ActionResult<Quote>> GetQuote(string id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
                return NotFound("Quote not found");

            return Ok(quote);
        }

        [HttpPost]
        public async Task<ActionResult<Quote>> CreateQuote(Quote quote)
        {
            _context.Quotes.Add(quote);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuoteExists(quote.QuoteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuote", new { id = quote.QuoteId }, quote);
        }

        [HttpPut("UpdateQuote/{id}")]
        public async Task<ActionResult> UpdateQuote(string id, Quote quote)
        {
            if (id != quote.QuoteId)
            {
                return BadRequest();
            }

            _context.Entry(quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(string id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuoteExists(string id)
        {
            return _context.Quotes.Any(e => e.QuoteId == id);
        }
    }
}
