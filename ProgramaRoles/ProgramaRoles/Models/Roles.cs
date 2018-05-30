using System.ComponentModel.DataAnnotations;

namespace ProgramaRoles.Models
{
    public class Roles
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string rol { get; set; }

        [Required]
        public string descripcion { get; set; }

    }
}