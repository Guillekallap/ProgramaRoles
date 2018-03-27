using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgramaRoles.Repository;
using ProgramaRoles.Utils;

namespace ProgramaRoles.Models
{
    public class ViewModelUsuarioRolHorario
    {
        public int id { get; set; }
        public int idUsuarioSector { get; set; }
        public string nombreUsuario { get; set; }
        public string rolesTemporales { get; set; }
        public string email { get; set; }
        public DateTime fechaActual { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public bool vigente { get; set; }
        public bool emailChked { get; set; }
        public bool Chked { get; set; }
        public string fechas { get; set; }
        public string horas { get; set; }

        public ViewModelUsuarioRolHorario(ViewModelUsuarioRol vMuestra)
        {
            this.idUsuarioSector = vMuestra.id;
            this.nombreUsuario = vMuestra.nombreUsuario;
            this.Chked = false;
            this.emailChked = false;
            this.email = vMuestra.email;
        }
    }

    

}