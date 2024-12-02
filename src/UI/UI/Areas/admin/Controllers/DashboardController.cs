using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.admin.Controllers
{
    [Route("{area}/{controller}/{action}")]
    public class DashboardController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
