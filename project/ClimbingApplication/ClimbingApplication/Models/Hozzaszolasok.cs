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
        [ForeignKey("UtHozzaszolas")]
        public int UtakID { get; set; }

        public virtual Utak? UtHozzaszolas { get; set; }
    }
}
