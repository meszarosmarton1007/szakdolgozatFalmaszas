using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Valaszok
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string  valasz { get; set; }

        [Required]
        [ForeignKey("Valasz")]
        [Display(Name = "Hozzaszolas")]
        public int HozzaszolasID { get; set; }

        public virtual Hozzaszolasok? Valasz { get; set; }

        [Required]
        [ForeignKey("Valasziro")]
        public int FelhasznaloID { get; set; }

        public virtual Felhasznalok? Valasziro { get; set; }
    }
}
