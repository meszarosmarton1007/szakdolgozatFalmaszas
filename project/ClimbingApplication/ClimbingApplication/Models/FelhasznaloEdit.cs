using System.ComponentModel.DataAnnotations;

namespace ClimbingApplication.Models
{
    public class FelhasznaloEdit
    {
        public int ID { get; set; }

       [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Vezetéknév")]
        public string vezetekNev { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Keresztnév")]
        public string KeresztNev { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Születési idő")]
        public DateOnly szuletesiIdo { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Telefonszám")]
        public string telefonszam {  get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Felhasználónév")]
        public string felhasznaloNev { get; set; }

        public string rang {  get; set; }

        [DataType(DataType.Password)]
        public string? ujJelszo { get; set; }

    }
}
