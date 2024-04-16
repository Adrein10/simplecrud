using Microsoft.AspNetCore.Mvc;
using simplecrud.Models;
using System.Diagnostics;

namespace simplecrud.Controllers
{
    public class HomeController : Controller
    {
       

        private readonly ILogger<HomeController> _logger;
        private readonly SMSContext context;

        public HomeController(ILogger<HomeController> logger ,SMSContext context)
        {
            _logger = logger;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Student std)
        {
            context.Students.Add(std);
            context.SaveChanges();
            return RedirectToAction("Students");
        }

        public IActionResult Students()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}