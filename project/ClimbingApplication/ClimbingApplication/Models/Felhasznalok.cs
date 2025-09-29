using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Felhasznalok
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Vezetéknév")]
        public string vezetekNev { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Keresztnév")]
        public string keresztNev { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [EmailAddress(ErrorMessage = "Érvénytelen e-mail cím.")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Jelszó")]
        public string jelszo { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Születési idő")]
        public DateOnly szuletesiIdo { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [RegularExpression(@"^(\+?[\d\s\-\(\)]{7,20})$", ErrorMessage = "A telefonszám formátuma érvénytelen. Kérjük, használjon számokat és szükség esetén + - ( ) karaktereket.")]
        [Display(Name = "Telefonszám")]
        public string telefonszam { get; set; }

        public string rang {  get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Felhasználónév")]
        public string felhasznaloNev { get; set; }
    }
}
