using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Utak
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string kep { get; set; }

        [Required]
        public string nehezseg { get; set; }

        [Required]
        public string nev { get; set; }

        [Required]
        public string leiras { get; set; }

        [Required]
        public DateOnly letrehozva { get; set; }

        [Required]
        [ForeignKey("Falonut")]
        public int FalID { get; set; }

        public virtual Falak? Falonut {  get; set; }

        public ICollection<Hozzaszolasok>? Hozzaszolasoks { get; set; }

        //public ICollection<Valaszok> Valaszoks { get; set; }
    }
}
