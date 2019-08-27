using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NPost.Infrastructure;

namespace NPost.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        private readonly IOptions<AppOptions> _options;

        public HomeController(IOptions<AppOptions> options)
        {
            _options = options;
        }

        [HttpGet]
        public ActionResult<string> Get() => _options.Value.Name;
    }
}