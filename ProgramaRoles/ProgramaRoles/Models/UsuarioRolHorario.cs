using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgramaRoles.Utils;

namespace ProgramaRoles.Models
{
    public class UsuarioRolHorario
    {
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

        public UsuarioRolHorario()
        {
        
        }

        public UsuarioRolHorario(ViewModelUsuarioRolHorario vmUsRolHorario)
        {
            this.idUsuarioSector = vmUsRolHorario.idUsuarioSector;
            this.nombreUsuario = vmUsRolHorario.nombreUsuario;
            this.rolesTemporales = vmUsRolHorario.rolesTemporales;
            this.email = vmUsRolHorario.email;
            this.fechaModificacion = DateTime.Now;
            this.fechaInicio = vmUsRolHorario.fechaInicio;
            this.fechaFin = vmUsRolHorario.fechaFin;
            this.vigente = (new UtilsString()).VerificarFechaVigenciaDeRol(fechaInicio);
            this.emailChked = vmUsRolHorario.emailChked;
        }



    }
}