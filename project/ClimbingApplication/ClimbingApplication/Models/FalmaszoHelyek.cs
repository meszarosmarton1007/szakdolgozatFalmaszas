using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class FalmaszoHelyek
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Ország")]
        public string orszag { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "A terem helye. A cím lehet postai cím vagy koordináta")]
        public string cim { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "A terem honlapja")]
        public string honlap { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "A terem neve")]
        public string nev { get; set; }
        
        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Néhány fontos információ a teremről")]
        public string leiras { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [ForeignKey("Hozzaado")]
        public int FelhasznalokID { get; set; }

        public virtual Felhasznalok? Hozzaado { get; set; }


        public virtual ICollection<Falak> Falak { get; set; } = new List<Falak>();


    }
}
