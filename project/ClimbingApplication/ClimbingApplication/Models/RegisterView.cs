using System.ComponentModel.DataAnnotations;

namespace ClimbingApplication.Models
{
    public class RegisterView
    {
        [Required]
        public string VezetekNev { get; set; }

        [Required]
        public string KeresztNev { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Jelszo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Jelszo", ErrorMessage = "A két jelszó nem egyezik meg")]
        public string JelszoMegerosites { get; set; }

        [Required]
        public DateOnly SzuletesiIdo { get; set; }

        [Required]
        [RegularExpression(@"^(\+?[\d\s\-\(\)]{7,20})$", ErrorMessage = "A telefonszám formátuma érvénytelen. Kérjük, használjon számokat és szükség esetén + - ( ) karaktereket.")]
        public string Telefonszam {  get; set; }

        [Required]
        public string FelhasznaloNev { get; set; }
    }
}
