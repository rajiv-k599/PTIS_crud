using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SimpleCrud.Data;
using SimpleCrud.Models;
using SimpleCrud.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext Context;

        private readonly IWebHostEnvironment WebHostEnvironment;

        // private AppDbContext db = new AppDbContext();
        public HomeController(AppDbContext _context,IWebHostEnvironment webHostEnvironment)
        {
            this.Context = _context;
            WebHostEnvironment = webHostEnvironment;
         
        }
       // private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        [HttpGet]
        public IActionResult Index()
        {
            return View();
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
            return RedirectToAction("StudentInfo");
        }
        public IActionResult Update(StudentVM update)
        {
            string stringFile = upload(update);

            if (update.Image != null)
            {
                var student = Context.Students.Find(update.Id);
                student.Name = update.Name;
                student.Email = update.Email;
                student.Address = update.Address;
                student.Phone = update.Phone;
                student.Gender = update.Gender;
                student.Dob = update.Dob;
                student.Faculty = update.Faculty;
                student.Semester = update.Semester;
                student.Eroll = update.Eroll;
                student.Reg = update.Reg;
                student.Image = stringFile;
                Context.SaveChanges();
                return RedirectToAction("StudentInfo");
            } else
            {
                var student = Context.Students.Find(update.Id);
                student.Name = update.Name;
                student.Email = update.Email;
                student.Address = update.Address;
                student.Phone = update.Phone;
                student.Gender = update.Gender;
                student.Dob = update.Dob;
                student.Faculty = update.Faculty;
                student.Semester = update.Semester;
                student.Eroll = update.Eroll;
                student.Reg = update.Reg;
               
                Context.SaveChanges();
                return RedirectToAction("StudentInfo");

            }
           // return View("View");
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}