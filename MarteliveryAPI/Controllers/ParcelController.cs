using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarteliveryAPI.Data;
using MarteliveryAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MarteliveryAPI.Models.DTOs;
using System.Security.Claims;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ParcelController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ParcelController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Get method for admin to get all parcels info
        [HttpGet ("GetAllParcelsInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllParcelsInfo()
        {
            var parcels = await _context.Parcels.ToListAsync();

            if (parcels.Count == 0)
                return NotFound("Parcels not found");

            return Ok(parcels);
        }

        //Get method for admin to get parcel info by id
        [HttpGet ("GetParcelInfo/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetParcelInfo(string id)
        {
            var parcel = await _context.Parcels.FindAsync(id);

            if (parcel == null)
                return NotFound("Parcel not found");

            return Ok(parcel);
        }

        //Post method for user to create a parcel
        [HttpPost ("CreateParcel")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateParcel(ParcelDTO parcelDTO)
        {
            var parcel = _mapper.Map<Parcel>(parcelDTO);
            parcel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Parcels.Add(parcel);
            await _context.SaveChangesAsync();

            return Ok("Parcel created");
        }

        //Put method for user to update his parcel
        [HttpPut ("UpdateParcel/{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateParcel(string id, ParcelDTO parcelDTO)
        {
            var parcel = await _context.Parcels.FindAsync(id);

            //Check if Customer is the owner of the parcel
            if (parcel.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Unauthorized("You are not the owner of this parcel");

            //Check if parcel exists
            if (parcel == null)
                return NotFound("Parcel not found");

            parcel = _mapper.Map(parcelDTO, parcel);

            _context.Parcels.Update(parcel);
            await _context.SaveChangesAsync();

            return Ok("Parcel updated");
        }

        //Delete method for admin to delete a parcel by id
        [HttpDelete ("DeleteParcel/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteParcel(string id)
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
