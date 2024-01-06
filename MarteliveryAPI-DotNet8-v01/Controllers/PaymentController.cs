using MarteliveryAPI_DotNet8_v01.Data;
using Microsoft.AspNetCore.Mvc;

namespace MarteliveryAPI_DotNet8_v01.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController(DataContext context) : ControllerBase
    {
    }
}
