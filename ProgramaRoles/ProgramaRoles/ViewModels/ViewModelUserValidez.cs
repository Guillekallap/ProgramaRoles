using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramaRoles.ViewModels
{
    public class ViewModelUserValidez
    {
        public List<ViewModel> listaUsuario { get; set; }
        public List<ViewModelUsuarioRolHorario> listaUsuarioRolHorario { get; set; }
        public List<ViewModelUsuarioRolHorario> listaUsuarioRolHorarioInvalido { get; set; }

        public ViewModelUserValidez() {  }

    }
}