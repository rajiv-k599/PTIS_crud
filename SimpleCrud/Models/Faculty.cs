using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCrud.Models
{
    [Table("Facultyies")]
    public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? FacultyName { get; set; } = default;
    }
}
