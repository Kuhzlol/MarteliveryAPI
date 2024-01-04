using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarteliveryAPI_DotNet8_v01.Data;
using MarteliveryAPI_DotNet8_v01.Models;

namespace MarteliveryAPI_DotNet8_v01.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParcelController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet ("GetParcelsInfo")]
        public async Task<ActionResult<List<Parcel>>> GetParcelsInfo()
        {
            var parcels = await _context.Parcels.ToListAsync();

            if (parcels.Count == 0)
                return NotFound("Parcels not found");

            return Ok(parcels);
        }

        [HttpGet ("GetParcelInfo/{id}")]
        public async Task<ActionResult<Parcel>> GetParcelInfo(string id)
        {
            var parcel = await _context.Parcels.FindAsync(id);

            if (parcel == null)
                return NotFound("Parcel not found");

            return Ok(parcel);
        }

        [HttpPost ("CreateParcel")]
        public async Task<ActionResult<Parcel>> CreateParcel(Parcel parcel)
        {
            _context.Parcels.Add(parcel);
            await _context.SaveChangesAsync();

            return Ok("Parcel created");
        }

        [HttpPut ("UpdateParcel/{id}")]
        public async Task<IActionResult> UpdateParcel(Guid id, Parcel parcel)
        {
            var parcelToUpdate = await _context.Parcels.FindAsync(id);

            if (parcelToUpdate == null)
                return NotFound("Parcel not found");

            bool isUpdated = false;
            if (parcelToUpdate.PickupLocation != parcel.PickupLocation)
            {
                parcelToUpdate.PickupLocation = parcel.PickupLocation;
                isUpdated = true;
            }
            if (parcelToUpdate.DeliveryLocation != parcel.DeliveryLocation)
            {
                parcelToUpdate.DeliveryLocation = parcel.DeliveryLocation;
                isUpdated = true;
            }
            if (parcelToUpdate.Length != parcel.Length)
            {
                parcelToUpdate.Length = parcel.Length;
                isUpdated = true;
            }
            if (parcelToUpdate.Width != parcel.Width)
            {
                parcelToUpdate.Width = parcel.Width;
                isUpdated = true;
            }
            if (parcelToUpdate.Height != parcel.Height)
            {
                parcelToUpdate.Height = parcel.Height;
                isUpdated = true;
            }
            if (parcelToUpdate.Weight != parcel.Weight)
            {
                parcelToUpdate.Weight = parcel.Weight;
                isUpdated = true;
            }

            if (isUpdated)
            {
                await _context.SaveChangesAsync();
                return Ok("Parcel updated");
            }
            else 
            {
                return Ok("No changes were made to the parcel");
            }
        }

        [HttpDelete ("DeleteParcel/{id}")]
        public async Task<ActionResult> DeleteParcel(string id)
        {
            var parcel = await _context.Parcels.FindAsync(id);
            
            if (parcel == null)
                return NotFound("Parcel not found");

            _context.Parcels.Remove(parcel);
            await _context.SaveChangesAsync();

            return Ok("Parcel deleted");
        }
    }
}
