using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimbingApplication.Models
{
    public class Hozzaszolasok
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string hozzaszolas { get; set; }

        [Required]
        [ForeignKey("Valsz")]
        public int ValaszokID { get; set; }

        public virtual Hozzaszolasok? Valasz { get; set; }
    }
}
