using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Falak
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Név")]
        public string nev { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Kép")]
        public string kep { get; set; }


        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Érvényesség kezdete")]
        public DateOnly letrehozva { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [ForeignKey("Falhelye")]
        public int FalmaszohelyID { get; set; }

        public virtual FalmaszoHelyek? Falhelye { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [ForeignKey("Letrehozo")]
        public int FelhasznaloID { get; set; }

        public virtual Felhasznalok? Letrehozo { get; set; }

        public virtual ICollection<Utak> Utak { get; set; } = new List<Utak>();
    }
}
