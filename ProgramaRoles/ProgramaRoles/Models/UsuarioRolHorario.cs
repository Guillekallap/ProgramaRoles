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
        public string rolesAnteriores { get; set; }
        public string rolesNuevos { get; set; }
        public string email { get; set; }
        public DateTime fechaActual { get; set; }
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
            this.rolesAnteriores = vmUsRolHorario.rolesAnteriores;
            this.rolesNuevos = vmUsRolHorario.rolesNuevos;
            this.email = vmUsRolHorario.email;
            this.fechaActual = DateTime.Now;
            this.fechaInicio = vmUsRolHorario.fechaInicio;
            this.fechaFin = vmUsRolHorario.fechaFin;
            this.vigente = (new UtilsString()).verificarFechaVigenciaDeRol(fechaInicio);
            this.emailChked = vmUsRolHorario.emailChked;
        }



    }
}