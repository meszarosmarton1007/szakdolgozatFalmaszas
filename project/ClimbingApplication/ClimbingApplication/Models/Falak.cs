using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Falak
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string nev { get; set; }

        [Required]
        public string kep { get; set; }


        [Required]
        public DateOnly letrehozva { get; set; }

        [Required]
        [ForeignKey("Falonut")]
        public int FalID { get; set; }

        public virtual Utak? Falonut { get; set; }
    }
}
