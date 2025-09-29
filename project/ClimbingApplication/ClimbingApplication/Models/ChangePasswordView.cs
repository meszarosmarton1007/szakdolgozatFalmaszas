using System.ComponentModel.DataAnnotations;

namespace ClimbingApplication.Models
{
    public class ChangePasswordView
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Jelenlegi jelszó")]
        public string JelenlegiJelszo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Új jelszó")]
        public string UjJelszo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("UjJelszo", ErrorMessage = "Az új jelszavak nem egyeznek meg")]
        [Display(Name = "Új jelszó megint")]
        public string JelszoMegerosito { get; set; }
    }
}
