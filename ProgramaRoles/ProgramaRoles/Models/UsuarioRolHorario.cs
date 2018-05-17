using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgramaRoles.Utils;

namespace ProgramaRoles.Models
{
    public class UsuarioRolHorario
    {
        public UsuarioRolHorario()
        {

        }
        public int id { get; set; }
        public int idUsuarioSector { get; set; }
        public string nombreUsuario { get; set; }
        public string rolesTemporales { get; set; }
        public string email { get; set; }
        public DateTime fechaModificacion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public bool vigente { get; set; }
        public bool emailChked { get; set; }
 
        public UsuarioRolHorario(int idUsuarioSector, string nombreUsuario, string rolesTemporales, string email, DateTime fechaInicio, DateTime fechaFin, bool emailChked)
        {
            this.idUsuarioSector = idUsuarioSector;
            this.nombreUsuario = nombreUsuario;
            this.rolesTemporales = rolesTemporales;
            this.email = email;
            this.fechaModificacion = DateTime.Now;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.vigente = (new UtilsString()).VerificarFechaVigenciaDeRol(fechaInicio);
            this.emailChked = emailChked;
        }



    }
}