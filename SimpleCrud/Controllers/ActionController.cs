using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCrud.Data;
using SimpleCrud.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using SimpleCrud.ViewModel;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace SimpleCrud.Controllers
{
    public class ActionController : Controller
    {
        private readonly AppDbContext Context;
        private readonly INotyfService _notyf;

        private readonly IWebHostEnvironment WebHostEnvironment;

        // private AppDbContext db = new AppDbContext();
        public ActionController(AppDbContext _context, IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            this.Context = _context;
            WebHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }
        public IActionResult AddInfo(Student s)
        {

            
            return View();
        }
        [HttpGet]
        public IActionResult View(int id)
        {
            //var std= Context.Students.FirstOrDefault(x => x.Id == id);
            //if (std == null)
            //{
            //    var viewModel = new UpdateVm()
            //    {
            //        Id = std.Id,
            //        Name = std.Name,
            //        Email = std.Email,
            //        Address = std.Address,
            //        Phone = std.Phone,
            //        Gender = std.Gender,
            //        Dob = std.Dob,
            //        Faculuy = std.Faculty,
            //        Semester = std.Semester,
            //        Eroll = std.Eroll,
            //        Reg = std.Reg


            //    };
            //}
            


            return View();
        }
    }
}
