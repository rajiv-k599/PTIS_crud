using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleCrud.Data;
using SimpleCrud.Models;
using SimpleCrud.ViewModel;
using System.Diagnostics;

namespace SimpleCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext Context;

       // private AppDbContext db = new AppDbContext();
        public HomeController(AppDbContext _context)
        {
            this.Context = _context;
         
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
        public IActionResult AddInfo(Student s)
        {
            //ViewBag.data = s;
            this.Context.Students.Add(s);

            this.Context.SaveChanges();
        //   var a= Context.Students.ToList();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult StudentInfo()
        {
            //  StudentVM obj=new StudentVM();
            //  obj.students = Context.Students.ToList();
            var std = Context.Students.ToList();

            return View(std);
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
                    Reg = std.Reg
                };
                return View(viewModel);
            


          

        }
        public IActionResult Delete(int? id)
        {
            var data = Context.Students.Find(id);
            Context.Students.Remove(data);
            Context.SaveChanges();
            return RedirectToAction("StudentInfo");
        }
        public IActionResult Update(UpdateVm update)
        {
            var student=Context.Students.Find(update.Id);
              student.Name = update.Name;
                student.Email=update.Email;
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
            
           // return View("View");
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}