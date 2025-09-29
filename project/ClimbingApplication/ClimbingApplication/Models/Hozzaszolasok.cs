using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Hozzaszolasok
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Hozzászólás")]
        public string hozzaszolas { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [ForeignKey("UtHozzaszolas")]
        public int UtakID { get; set; }

        public virtual Utak? UtHozzaszolas { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [ForeignKey("UtHozzaszolo")]
        public int FelhasznaloID { get; set; }

        public virtual Felhasznalok? UtHozzaszolo { get; set; }

        public ICollection<Valaszok>? Valaszok { get; set; }
        
    }
}
