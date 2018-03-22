using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgramaRoles.Utils;

namespace ProgramaRoles.Models
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

        public ViewModelUsuarioRol(UsuariosSectores usec,List<Roles> RolesTotales)
        {
            this.id = usec.id;
            this.nombreSector = usec.nombreSector;
            this.nombreUsuario = usec.nombreUsuario;
            this.email = usec.email;
            UtilsString Auxiliar = new UtilsString();
            List<string> lista_string =Auxiliar.ConvertirDeListaDeRolesAListaNombreRoles(RolesTotales);
            this.nombreRoles = Auxiliar.ParsearPropiedadRoles(usec,lista_string);
        }
        

    }
 
}
