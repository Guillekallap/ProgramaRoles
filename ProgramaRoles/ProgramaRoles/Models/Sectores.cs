using System.ComponentModel.DataAnnotations;

namespace ProgramaRoles.Models
{
    public class Sectores
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [StringLength(30)]
        public string nombreAbreviado { get; set; }

        public int orden { get; set; }

        [StringLength(7)]
        public string tipo { get; set; }

        public bool vigente { get; set; }

    }
}