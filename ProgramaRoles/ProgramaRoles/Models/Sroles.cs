using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramaRoles.Models
{
    public class Sroles
    {
        public Sroles()
        {

        }

        public Sroles(Roles roles, bool RolSeleccionado)
        {
            this.roles = roles;
            this.RolSeleccionado = RolSeleccionado;
        }

        public bool RolSeleccionado { get; set; }
        public Roles roles { get; set; }
    }
}