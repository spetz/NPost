using Microsoft.AspNetCore.Mvc;

namespace NPost.Modules.Deliveries.Controllers
{
    [ApiController]
    [Route("api/deliveries")]
    public class DeliveriesApiController : ControllerBase
    {
        [HttpGet("_meta")]
        public ActionResult<string> Meta() => "Deliveries module";
    }
}