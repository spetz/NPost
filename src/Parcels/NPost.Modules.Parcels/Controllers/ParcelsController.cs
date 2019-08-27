using Microsoft.AspNetCore.Mvc;

namespace NPost.Modules.Parcels.Controllers
{
    [Route("[controller]")]
    public class ParcelsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}