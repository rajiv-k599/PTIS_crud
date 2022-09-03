using System.ComponentModel.DataAnnotations;

namespace SimpleCrud.ViewModel
{
    public class AdminVM
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "please enter username")]
        public string? AdminName { get; set; } = default;

        [Required(ErrorMessage = "Please enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter Mobilenumber")]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Mobile number is not valid.")]
        public long? Phone { get; set; } = default;

        [Required(ErrorMessage = "please enter Gender")]
        public string? Gender { get; set; } = default;

        [Required(ErrorMessage = "please enter password")]
        public string Password { get; set; }
    }
}
