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
        public string telefonszam { get; set; }

        public string rang {  get; set; }


        [Required]
        [ForeignKey("HelyHozzaado")]
        public int HelyID { get; set; }

        public virtual FalmaszoHelyek? HelyHozzaado { get; set; }

        [Required]
        [ForeignKey("Hozzaszolo")]
        public int HozzaszolasokID { get; set; }

        public virtual Hozzaszolasok? Hozzaszolo { get; set; }

        [Required]
        [ForeignKey("Valszolo")]
        public int ValaszokID { get; set; }

        public virtual Hozzaszolasok? Valaszolo { get; set; }
    }
}
