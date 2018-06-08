using System.Collections.Generic;
using ProgramaRoles.Models;
using ProgramaRoles.Utils;

namespace ProgramaRoles.ViewModels
{
    public class ViewModelUsuarioRol
    {
        public ViewModelUsuarioRol(){

        }

        public int id { get; set; }
        public string nombreUsuario { get; set; }
        public string nombreSector { get; set; }
        public string email { get; set; }
        public List<Sroles> nombreRoles { get; set; }
        public string roles { get; set; }

        public ViewModelUsuarioRol(UsuariosSectores usec,List<Roles> RolesTotales)
        {
            this.id = usec.id;
            this.nombreSector = usec.nombreSector;
            this.nombreUsuario = usec.nombreUsuario;
            this.email = usec.email;
            UtilsRoles Auxiliar = new UtilsRoles();
            List<string> lista_string =Auxiliar.ConvertirDeListaDeRolesAListaNombreRoles(RolesTotales);
            this.nombreRoles = Auxiliar.ParsearPropiedadRoles(usec,lista_string);
            this.roles = usec.roles;
        }
        

    }
 
}
