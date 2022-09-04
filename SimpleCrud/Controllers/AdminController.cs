using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SimpleCrud.Data;
using SimpleCrud.Models;
using SimpleCrud.ViewModel;

namespace SimpleCrud.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext Context;
        private readonly INotyfService _notyf;

        private readonly IWebHostEnvironment WebHostEnvironment;

        // private AppDbContext db = new AppDbContext();
        public AdminController(AppDbContext _context, IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            this.Context = _context;
            WebHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterInfo(AdminVM A)
        {
           
            if (ModelState.IsValid)
            {
                string password = BCrypt.Net.BCrypt.HashPassword(A.Password);
                var reg = new Admin
                {
                    AdminName = A.AdminName,
                    Email = A.Email,
                    Gender = A.Gender,
                    Phone = A.Phone,
                    Password = password
                };
                Context.Admins.Add(reg);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "";

            }
            return View("Register");
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies=Request.Cookies.Keys;
            foreach (var cookies in storedCookies)
            {
                Response.Cookies.Delete(cookies);
            }
                  
            return RedirectToAction("Index");
        }
       
    
    
    }
}
