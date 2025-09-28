using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Felhasznalok
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string vezetekNev { get; set; }

        [Required]
        public string keresztNev { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [EmailAddress(ErrorMessage = "Érvénytelen e-mail cím.")]
        public string email { get; set; }

        [Required]
        public string jelszo { get; set; }

        [Required]
        public DateOnly szuletesiIdo { get; set; }

        [Required]
        [RegularExpression(@"^(\+?[\d\s\-\(\)]{7,20})$", ErrorMessage = "A telefonszám formátuma érvénytelen. Kérjük, használjon számokat és szükség esetén + - ( ) karaktereket.")]
        public string telefonszam { get; set; }

        public string rang {  get; set; }

        [Required]
        public string felhasznaloNev { get; set; }
    }
}
