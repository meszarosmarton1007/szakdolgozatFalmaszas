using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Valaszok
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Válasz")]
        public string  valasz { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [ForeignKey("Valasz")]
        [Display(Name = "Hozzaszolas")]
        public int HozzaszolasID { get; set; }

        public virtual Hozzaszolasok? Valasz { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [ForeignKey("Valasziro")]
        public int FelhasznaloID { get; set; }

        public virtual Felhasznalok? Valasziro { get; set; }
    }
}
