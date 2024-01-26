using AutoMapper;
using MarteliveryAPI.Data;
using MarteliveryAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrierRatingController(DataContext context, IMapper mapper) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        /*---------*/
        /*  ADMIN  */
        /*---------*/

        /*-----------*/
        /*  CARRIER  */
        /*-----------*/

        /*------------*/
        /*  CUSTOMER  */
        /*------------*/
    }
}
