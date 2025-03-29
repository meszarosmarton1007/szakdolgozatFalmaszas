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
        [ForeignKey("Hosszaszolas")]
        public int HozzaszolasokID { get; set; }

        public virtual Hozzaszolasok? Hozzaszolas {  get; set; }


    }
}
