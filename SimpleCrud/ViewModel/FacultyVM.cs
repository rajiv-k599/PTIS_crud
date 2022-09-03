using System.ComponentModel.DataAnnotations;
using SimpleCrud.Models;

namespace SimpleCrud.ViewModel
{
    public class FacultyVM
    {
        
        public int Id { get; set; }

       
        public string? FacultyName { get; set; } = default;
    }
}
