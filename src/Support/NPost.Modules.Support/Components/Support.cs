using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NPost.Modules.Support.Components
{
    public class Support : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View();
        }
    }
}