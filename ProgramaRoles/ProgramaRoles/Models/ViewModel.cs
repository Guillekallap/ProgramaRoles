using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramaRoles.Models
{
    public class ViewModel
    {
        public ViewModel()
        {

        }

        public int Id { get; set; }
        public string nombreSec { get; set; }

        public string nombreUsu { get; set; }

        public string dni { get; set; }

        public bool Chked { get; set; }

        
        public ViewModel(UsuariosSectores usec)
        {
            this.Id = usec.id;
            this.nombreSec = usec.nombreSector;
            this.nombreUsu = usec.nombreUsuario;
            this.dni = usec.dni;
            this.Chked = false;
        }
    }
}




