using MarteliveryAPI_DotNet8_v01.Data;
using MarteliveryAPI_DotNet8_v01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI_DotNet8_v01.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet ("GetPaymentsInfo")]
        public async Task<ActionResult<List<Payment>>> GetPaymentsInfo()
        {
            var payments = await _context.Payments.ToListAsync();

            if (payments.Count == 0)
                return NotFound("Payments not found");
            
            return Ok(payments);
        }

        [HttpGet("GetPayment/{id}")]
        public async Task<ActionResult<Payment>> GetPaymentInfo(string id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return NotFound("Payment not found");

            return Ok(payment);
        }

        [HttpPost ("CreatePayment")]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok("Payment created");
        }

        [HttpPut ("UpdatePayment/{id}")]
        public async Task<IActionResult> UpdatePayment(Guid id, Payment payment)
        {
            var paymentToUpdate = await _context.Payments.FindAsync(id);

            if (paymentToUpdate == null)
                return NotFound("Payment not found");

            bool isUpdated = false;
            if (paymentToUpdate.PaymentMethod != payment.PaymentMethod)
            {
                paymentToUpdate.PaymentMethod = payment.PaymentMethod;
                isUpdated = true;
            }
            if (paymentToUpdate.PaymentStatus != payment.PaymentStatus)
            {
                paymentToUpdate.PaymentStatus = payment.PaymentStatus;
                isUpdated = true;
            }
            if (paymentToUpdate.PaymentAmount != payment.PaymentAmount)
            {
                paymentToUpdate.PaymentAmount = payment.PaymentAmount;
                isUpdated = true;
            }            

            if (isUpdated)
            {
                _context.Payments.Update(paymentToUpdate);
                await _context.SaveChangesAsync();
                return Ok("Payment updated");
            }
            
            return Ok("No changes were made to the payment");
        }

        [HttpDelete ("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            var paymentToDelete = await _context.Payments.FindAsync(id);

            if (paymentToDelete == null)
                return NotFound("Payment not found");

            _context.Payments.Remove(paymentToDelete);
            await _context.SaveChangesAsync();

            return Ok("Payment deleted");
        }
    }
}
