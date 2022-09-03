using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCrud.Data;
using SimpleCrud.Models;
using SimpleCrud.ViewModel;

namespace SimpleCrud.Controllers
{
    public class FacultyController : Controller
    {
        private readonly AppDbContext Context;
        private readonly INotyfService _notyf;

        private readonly IWebHostEnvironment WebHostEnvironment;

        // private AppDbContext db = new AppDbContext();
        public FacultyController(AppDbContext _context, IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            this.Context = _context;
            WebHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(Context.Faculties.ToList());
        }
        public IActionResult AddFaculty(FacultyVM faculty)
        {
            var facul = new Faculty
            {
                FacultyName = faculty.FacultyName
            };
            Context.Faculties.Add(facul);
            Context.SaveChanges();
            _notyf.Success("Success Notification");
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            var data = Context.Faculties.Find(id);
            Context.Faculties.Remove(data);
            Context.SaveChanges();
            _notyf.Error("Success Notification");
            return RedirectToAction("Index");
        }
        public IActionResult UpdateFaculty(FacultyVM faculty)
        {
            var data= Context.Faculties.Find(faculty.Id);
            data.FacultyName=faculty.FacultyName;
            Context.Faculties.Update(data);
            Context.SaveChanges();
            _notyf.Success("Success Notification");
            return RedirectToAction("Index");
        }

    }
}
