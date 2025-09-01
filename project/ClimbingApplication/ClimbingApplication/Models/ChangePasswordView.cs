using System.ComponentModel.DataAnnotations;

namespace ClimbingApplication.Models
{
    public class ChangePasswordView
    {
        [Required]
        [DataType(DataType.Password)]
        public string JelenlegiJelszo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string UjJelszo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("UjJelszo", ErrorMessage = "Az új jelszavak nem egyeznek meg")]
        public string JelszoMegerosito { get; set; }
    }
}
