using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramaRoles.Models
{
    public class ViewModelUsuarioMuestra
    {
            public List<ViewModelUsuarioRol> listaUsuarioRol { get; set; }
            public List<ViewModelUsuarioRolHorario> listaUsuarioRolHorario { get; set; }
    }
}