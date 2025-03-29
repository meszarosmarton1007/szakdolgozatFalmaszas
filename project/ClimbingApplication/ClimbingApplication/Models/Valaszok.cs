using System.ComponentModel.DataAnnotations;

namespace ClimbingApplication.Models
{
    public class Valaszok
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string  valasz { get; set; }
    }
}
