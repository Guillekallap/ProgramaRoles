using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgramaRoles.Utils;

namespace ProgramaRoles.Models
{
    public class ViewMuestra
    {
        public ViewMuestra(){

        }

        public int id { get; set; }
        public string nombreUsuario { get; set; }
        public string nombreSector { get; set; }
        public List<Sroles> nombreRoles { get; set; }

        public ViewMuestra(UsuariosSectores usec,List<Roles> RolesTotales)
        {
            this.id = usec.id;
            this.nombreSector = usec.nombreSector;
            this.nombreUsuario = usec.nombreUsuario;
            UtilsString Auxiliar = new UtilsString();
            List<string> lista_string =Auxiliar.ConvertirDeListaDeRolesAListaNombreRoles(RolesTotales);
            this.nombreRoles = Auxiliar.ParsearPropiedadRoles(usec,lista_string);
        }
        

    }
 
}
