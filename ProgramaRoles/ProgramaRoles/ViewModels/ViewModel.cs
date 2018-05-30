using ProgramaRoles.Utils;
using ProgramaRoles.Models;

namespace ProgramaRoles.ViewModels
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
        public string roles { get; set; }
        public bool Chked { get; set; }

        
        public ViewModel(UsuariosSectores usec)
        {
            this.Id = usec.id;
            this.nombreSec = usec.nombreSector;
            this.nombreUsu = usec.nombreUsuario;
            this.dni = usec.dni;
            this.roles = usec.roles;
            this.Chked = false;
        }

        public ViewModel(UsuariosSectores usec, string rolSeleccionado)
        {
            this.Id = usec.id;
            this.nombreSec = usec.nombreSector;
            this.nombreUsu = usec.nombreUsuario;
            this.dni = usec.dni;
            this.roles = usec.roles;
            this.Chked = (new UtilsString()).VerificarRolEnUsuarioSector(usec,rolSeleccionado);
        }
    }
}




