using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NPost.Modules.Parcels.Components
{
    public class Parcels : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View();
        }
    }
}