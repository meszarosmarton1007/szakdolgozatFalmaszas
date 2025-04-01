using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class FalmaszoHelyek
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string orszag { get; set; }

        [Required]
        public string cim { get; set; }

        [Required]
        public string honlap { get; set; }

        public string koordinata { get; set; }
        
        [Required]
        public string leiras { get; set; }

        [Required]
        [ForeignKey("Hozzaado")]
        public int FelhasznalokID { get; set; }

        public virtual Felhasznalok? Hozzaado { get; set; }

        //TODO: Fal külső kulcs hozzáadása

    }
}
