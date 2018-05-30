using System;
using System.ComponentModel.DataAnnotations;

namespace ProgramaRoles.Models
{
    public class Usuarios
    {
        public int id { get; set; }

        public int idTipoDocumento { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nro documento")]
        [StringLength(8)]
        public string numeroDocumento { get; set; }

        public int idSexo { get; set; }

        [Required(ErrorMessage = "Debe ingresar un primer apellido")]
        [StringLength(100)]
        public string primerApellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar un primer nombre")]
        [StringLength(100)]
        public string primerNombre { get; set; }

        [StringLength(100)]
        public string otrosNombres { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        [StringLength(255)]
        public string nombreUsuario { get; set; }

        [StringLength(255)]
        public string contraseña { get; set; }

        public int? idProfesional { get; set; }

        public DateTime? ultimoIngreso { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        public bool vigente { get; set; }


        [StringLength(50)]
        public string telefono { get; set; }

        [StringLength(1)]
        public string tipoUsuario { get; set; }

    }
}