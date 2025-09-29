using System.ComponentModel.DataAnnotations;

namespace ClimbingApplication.Models
{
    public class RegisterView
    {
        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Vezetéknév")]
        public string VezetekNev { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Keresztnév")]
        public string KeresztNev { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Jelszo { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [DataType(DataType.Password)]
        [Compare("Jelszo", ErrorMessage = "A két jelszó nem egyezik meg")]
        [Display(Name = "Jelszó megerősítése")]
        public string JelszoMegerosites { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Születési idő")]
        public DateOnly SzuletesiIdo { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [RegularExpression(@"^(\+?[\d\s\-\(\)]{7,20})$", ErrorMessage = "A telefonszám formátuma érvénytelen. Kérjük, használjon számokat és szükség esetén + - ( ) karaktereket.")]
        [Display(Name = "Telefonszám")]
        public string Telefonszam {  get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Felhasználónév")]
        public string FelhasznaloNev { get; set; }
    }
}
