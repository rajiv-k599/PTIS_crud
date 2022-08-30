using SimpleCrud.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrud.ViewModel
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; } = default;
        public string? Faculty { get; set; } = default;
        public int Phone { get; set; } = default;

        public DateTime Dob { get; set; }
       
        public string Semester { get; set; }

        
        public DateTime Eroll { get; set; }

        
        public int Reg { get; set; } = default;

        public IFormFile? Image { get; set; }
        //public List<Student> students { get; set; }
    }
}
