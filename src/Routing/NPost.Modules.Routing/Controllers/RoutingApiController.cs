using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPost.Modules.Routing.Shared.DTO;
using NPost.Modules.Routing.Shared.Queries;
using NPost.Shared;

namespace NPost.Modules.Routing.Controllers
{
    [ApiController]
    [Route("api/routing")]
    public class RoutingApiController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public RoutingApiController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("_meta")]
        public ActionResult<string> Meta() => "Routing module";

        [HttpGet]
        public async Task<ActionResult<RouteDto>> Get([FromQuery] GetRoute query)
        {
            var route = await _dispatcher.QueryAsync(query);
            if (route is null)
            {
                return NotFound();
            }

            return Ok(route);
        }
    }
}