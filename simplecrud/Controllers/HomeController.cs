﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Login");
        }
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var login = context.Users.FirstOrDefault(options => options.Email == user.Email && options.Password == user.Password);
            if(login != null)
            {
                if(login.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if(login.Role == "Customer")
                {
                    return RedirectToAction("Index", "Customer");
                }
            }else{
                ViewBag.loginfailed = "Login failed";
                return View();
            }
            return View();
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
            if(ModelState.IsValid)
            {
                context.Students.Update(student);
                context.SaveChanges();
                return RedirectToAction("Students");
            }
            return View();
        }
        

        public IActionResult Delete(int id)
        {
            if(ModelState.IsValid)
            {
                var show = context.Students.FirstOrDefault(item => item.Id == id);
                context.Students.Remove(show);
                context.SaveChanges();
                return RedirectToAction("Students");
            }
            return RedirectToAction("Students");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}