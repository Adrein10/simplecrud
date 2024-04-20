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
            if (ModelState.IsValid)
            {
                context.Students.Add(std);
                context.SaveChanges();
                return RedirectToAction("Students");
            }
            return View("Index");
        }

        public IActionResult Students()
        {
            var show = context.Students.ToList();
            return View(show);
        }
        public IActionResult Edit(int id)
        {
            var show = context.Students.Find(id);
            return View(show);
        }
        [HttpPost]
        public IActionResult Edit(Student student,int id)
        {
            context.Students.Update(student);
            context.SaveChanges();
            return RedirectToAction("Students");
        }
        [HttpPost]
        public IActionResult Delete(Student student,int id)
        {
            context.Students.Remove(student);
            context.SaveChanges();
            return RedirectToAction("Students");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}