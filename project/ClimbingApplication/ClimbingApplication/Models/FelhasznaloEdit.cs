using System.ComponentModel.DataAnnotations;

namespace ClimbingApplication.Models
{
    public class FelhasznaloEdit
    {
        public int ID { get; set; }

       [Required]
       public string vezetekNev { get; set; }

        [Required]
        public string KeresztNev { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public DateOnly szuletesiIdo { get; set; }

        [Required]
        public string telefonszam {  get; set; }

        [Required]
        public string felhasznaloNev { get; set; }

        public string rang {  get; set; }

        [DataType(DataType.Password)]
        public string? ujJelszo { get; set; }

    }
}
