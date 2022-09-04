using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using NToastNotify;
using SimpleCrud.Data;
using SimpleCrud.Models;
using SimpleCrud.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SimpleCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext Context;
        private readonly INotyfService _notyf;

        private readonly IWebHostEnvironment WebHostEnvironment;

        // private AppDbContext db = new AppDbContext();
        public HomeController(AppDbContext _context,IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            this.Context = _context;
            WebHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }

        // private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View(Context.Faculties.ToList());
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var data = Context.Admins.Where(e => e.Email == model.Email).SingleOrDefault();
                if (data != null)
                {
                    bool isValid = (data.Email == model.Email && BCrypt.Net.BCrypt.Verify(model.Password,data.Password));
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, model.Email) }, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Email", data.Email);
                        HttpContext.Session.SetString("Name", data.AdminName);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["errorPassword"] = "Invalid Password!";
                        return RedirectToAction("Index","Admin");
                    }
                }
                else
                {
                    TempData["errorUsername"] = "Username not found!";
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

       
        [HttpPost]
        public IActionResult AddInfo(StudentVM s)
        {
            string stringFile = upload(s);

            var std = new Student{
                Name = s.Name,
                Email = s.Email,
                Address = s.Address,
                Phone = s.Phone,
                Gender = s.Gender,
                Dob = s.Dob,
                Faculty = s.Faculty,
                Semester = s.Semester,
                Eroll = s.Eroll,
                Reg = s.Reg,
                Image = stringFile            
            };

             Context.Students.Add(std);
             Context.SaveChanges();
            _notyf.Success("Inserted Success");
            //ViewBag.data = s;
            //    this.Context.Students.Add(s);

            //   this.Context.SaveChanges();
            //   var a= Context.Students.ToList();
            return RedirectToAction("Index");
        }

        private string upload(StudentVM s)
        {
            string fileName="";
            if (s.Image != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + s.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    s.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        [Authorize]
        [HttpGet]
        public IActionResult StudentInfo()
        {
             // StudentVM obj=new StudentVM();
            //var a=Context.Students.ToList();
         //    var std = Context.Students.ToList();
            //var std=obj.students;
            return View(Context.Students.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult View(int id)
        {
            var std = Context.Students.Find(id);

            ViewBag.model = Context.Faculties.ToList();
               var viewModel = new UpdateVm()
               {
                    Id = std.Id,
                    Name = std.Name,
                    Email = std.Email,
                    Address = std.Address,
                    Phone = std.Phone,
                    Gender = std.Gender,
                    Dob = std.Dob,
                    Faculty = std.Faculty,
                    Semester = std.Semester,
                    Eroll = std.Eroll,
                    Reg = std.Reg,
                    Image = std.Image
               };
                return View(viewModel);
           
        }
        public IActionResult Delete(int? id,string img)
        {
            string delDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images",img);
            FileInfo f1=new FileInfo(delDir);
            if (f1.Exists)
            {
                System.IO.File.Delete(delDir);
                f1.Delete();
            }
            var data = Context.Students.Find(id);
            Context.Students.Remove(data);
            Context.SaveChanges();
            _notyf.Error("Deleted Success");
            return RedirectToAction("StudentInfo");
        }
        public IActionResult Update(StudentVM update)
        {
            string stringFile = upload(update);

            //var student = Context.Students.Find(update.Id);
            ////  users = this.Context.Users.Find(Id);
            //string deletePath = Path.Combine(".\\wwwroot", "Images");
            //string fileDeletePath = Path.Combine(deletePath, student.Image);
            //FileInfo deleteFile = new FileInfo(fileDeletePath);
            //if (deleteFile.Exists)
            //{
            //    deleteFile.Delete();
            //}
            //student.Image = stringFile;
            //this.Context.Students.Update(student);
            //Context.SaveChanges();

            if (update.Image != null)
            {
                var student = Context.Students.Find(update.Id);
                string delDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images", student.Image);
                FileInfo f1 = new FileInfo(delDir);
                if (f1.Exists)
                {
                    System.IO.File.Delete(delDir);
                    f1.Delete();
                }
                student.Name = update.Name;
                student.Email = update.Email;
                student.Address = update.Address;
                student.Phone = (int)update.Phone;
                student.Gender = update.Gender;
                student.Dob = update.Dob;
                student.Faculty = update.Faculty;
                student.Semester = update.Semester;
                student.Eroll = update.Eroll;
                student.Reg = update.Reg;
                student.Image = stringFile;
                Context.SaveChanges();
                _notyf.Success("Updated Success");
                return RedirectToAction("StudentInfo");
            }
            else
            {
                var student = Context.Students.Find(update.Id);
                student.Name = update.Name;
                student.Email = update.Email;
                student.Address = update.Address;
                student.Phone = (int)update.Phone;
                student.Gender = update.Gender;
                student.Dob = update.Dob;
                student.Faculty = update.Faculty;
                student.Semester = update.Semester;
                student.Eroll = update.Eroll;
                student.Reg = update.Reg;
               
                Context.SaveChanges();
                _notyf.Success("Updated Success");
                return RedirectToAction("StudentInfo");

            }
            // return View("View");
          //  return RedirectToAction("StudentInfo");
        }

        public IActionResult Details(int id)
        {
            var std = Context.Students.Find(id);

            var viewModel = new UpdateVm()
            {
                Id = std.Id,
                Name = std.Name,
                Email = std.Email,
                Address = std.Address,
                Phone = std.Phone,
                Gender = std.Gender,
                Dob = std.Dob,
                Faculty = std.Faculty,
                Semester = std.Semester,
                Eroll = std.Eroll,
                Reg = std.Reg,
                Image = std.Image
            };
            return View(viewModel);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}