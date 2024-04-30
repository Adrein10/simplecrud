using Microsoft.AspNetCore.Mvc;

namespace simplecrud.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
