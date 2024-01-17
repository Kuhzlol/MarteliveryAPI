using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarteliveryAPI.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MarteliveryAPI.Models.DTOs.Admin;
using MarteliveryAPI.Models.Domain;
using MarteliveryAPI.Models.DTOs.Customer;

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

        /*---------*/
        /*  ADMIN  */
        /*---------*/

        //Get method for admin to get all parcels info with Mapped DTO
        [HttpGet ("AdminGetAllParcelsInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetAllParcelsInfo()
        {
            var parcels = await _context.Parcels.ToListAsync();

            if (parcels.Count == 0)
                return NotFound("Parcels not found");

            var parcelsDTO = _mapper.Map<List<AdminParcelDTO>>(parcels);

            return Ok(parcelsDTO);
        }

        //Get method for admin to get parcel info by id with Mapped DTO
        [HttpGet ("AdminGetParcelInfo/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminGetParcelInfo(string id)
        {
            var parcel = await _context.Parcels.FindAsync(id);

            if (parcel == null)
                return NotFound("Parcel not found");

            var parcelDTO = _mapper.Map<AdminParcelDTO>(parcel);

            return Ok(parcelDTO);
        }

        //Post method for admin to create a parcel with Mapped DTO
        [HttpPost("AdminCreateParcel")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCreateParcel(AdminParcelCreateDTO parcelDTO)
        {
            if (parcelDTO == null)
                return BadRequest("Model is empty");
            
            var parcel = _mapper.Map<Parcel>(parcelDTO);

            _context.Parcels.Add(parcel);
            await _context.SaveChangesAsync();

            return Ok("Parcel created");
        }

        //Put method for admin to update a parcel by id with Mapped DTO
        [HttpPut("AdminUpdateParcel/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateParcel(string id, AdminParcelUpdateDTO parcelDTO)
        {
            if (parcelDTO == null)
                return BadRequest("Model is empty");
            
            var parcel = await _context.Parcels.FindAsync(id);

            //Check if parcel exists
            if (parcel == null)
                return NotFound("Parcel not found");

            parcel = _mapper.Map(parcelDTO, parcel);

            _context.Parcels.Update(parcel);
            await _context.SaveChangesAsync();

            return Ok("Parcel updated");
        }

        //Delete method for admin to delete a parcel by id
        [HttpDelete("AdminDeleteParcel/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDeleteParcel(string id)
        {
            var parcel = await _context.Parcels.FindAsync(id);

            if (parcel == null)
                return NotFound("Parcel not found");

            _context.Parcels.Remove(parcel);
            await _context.SaveChangesAsync();

            return Ok("Parcel deleted");
        }

        /*---------*/
        /*  USERS  */
        /*---------*/

        //Get method for user to get all of their own parcels info with Mapped DTO
        [HttpGet ("GetMyParcelsInfo")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyParcelsInfo()
        {
            var parcels = await _context.Parcels.Where(p => p.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();

            if (parcels.Count == 0)
                return NotFound("Parcels not found");

            var parcelsDTO = _mapper.Map<List<CustomerParcelDTO>>(parcels);

            return Ok(parcelsDTO);
        }

        //Post method for user to create a parcel with Mapped DTO
        [HttpPost ("UserCreateParcel")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UserCreateParcel(CustomerParcelDTO parcelDTO)
        {
            var parcel = _mapper.Map<Parcel>(parcelDTO);

            parcel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Parcels.Add(parcel);
            await _context.SaveChangesAsync();

            return Ok("Parcel created");
        }

        //Put method for user to update his parcel by id with Mapped DTO
        [HttpPut ("UserUpdateParcel/{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UserUpdateParcel(string id, CustomerParcelDTO parcelDTO)
        {
            if (parcelDTO == null)
                return BadRequest("Model is empty");
            
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

        //Delete method for user to delete his parcel by id
        [HttpDelete ("UserDeleteParcel/{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UserDeleteParcel(string id)
        {
            var parcel = await _context.Parcels.FindAsync(id);

            //Check if Customer is the owner of the parcel
            if (parcel.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Unauthorized("You are not the owner of this parcel");

            if (parcel == null)
                return NotFound("Parcel not found");

            _context.Parcels.Remove(parcel);
            await _context.SaveChangesAsync();

            return Ok("Parcel deleted");
        }
    }
}
