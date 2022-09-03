using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCrud.Models
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? AdminName { get; set; } = default;

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public long? Phone { get; set; } = default;


        public string? Gender { get; set; } = default;

        [Required]
        public string Password { get; set; }
    }
}
