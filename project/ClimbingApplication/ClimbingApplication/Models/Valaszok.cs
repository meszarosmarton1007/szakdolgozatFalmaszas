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
        public int HozzaszolasID { get; set; }

        public virtual Hozzaszolasok? Valasz { get; set; }
    }
}
