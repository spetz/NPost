using Microsoft.AspNetCore.Mvc;

namespace NPost.Modules.Support.Controllers
{
    [ApiController]
    [Route("api/support")]
    public class SupportApiController : ControllerBase
    {
        [HttpGet("_meta")]
        public ActionResult<string> Meta() => "Support module";
    }
}