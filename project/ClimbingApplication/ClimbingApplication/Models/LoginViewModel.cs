using System.ComponentModel.DataAnnotations;

namespace ClimbingApplication.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Jelszo { get; set; }
    }
}
