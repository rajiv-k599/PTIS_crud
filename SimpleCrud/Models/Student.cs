using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCrud.Models
{
    [Table("student")]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; } = default;

        [Required]
        [StringLength(100)]
        public string? Email { get; set; } = default;

        [Required]
        [StringLength(100)]
        public string? Address { get; set; } = default;


        public string? Gender { get; set; } = default;

        [Required]
        public long Phone { get; set; } = default;

        [Required]
        public DateTime Dob { get; set; }
        public string? Faculty { get; set; } = default;
        public string? Semester { get; set; } = default;

        [Required]
        public DateTime Eroll { get; set; }

        [Required]
        public int Reg { get; set; } = default;

        public String? Image { get; set; }

       
    }
}
