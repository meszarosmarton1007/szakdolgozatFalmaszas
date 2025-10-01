using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Utak
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Kép")]
        public string kep { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Nehézség")]
        public string nehezseg { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "A út neve")]
        public string nev { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Leírás az útról")]
        public string leiras { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Display(Name = "Érvényesség kezdete")]
        public DateOnly letrehozva { get; set; }

        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [ForeignKey("Falonut")]
        public int FalID { get; set; }

        public virtual Falak? Falonut {  get; set; }

        //létehozó
        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [ForeignKey("UtLetrehozo")]
        public int FelhasznaloID { get; set; }

        public virtual Felhasznalok? UtLetrehozo { get; set; }

        public ICollection<Hozzaszolasok>? Hozzaszolasoks { get; set; }

        //public ICollection<Valaszok> Valaszoks { get; set; }
    }
}
