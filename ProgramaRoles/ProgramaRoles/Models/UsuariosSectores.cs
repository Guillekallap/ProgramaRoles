using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgramaRoles.Repository;
using ProgramaRoles.Utils;
using ProgramaRoles.Models;



namespace ProgramaRoles.Models
{
    public class UsuariosSectores
    {
        public UsuariosSectores()
        {

        }
        public int id { get; set; }
        public int idSector { get; set; }
        public string nombreSector { get; set; }
        public int idUsuario { get; set; }
        public string nombreUsuario {get; set;}

        public string dni { get; set; }
        public string roles { get; set; }
        public UsuariosSectores(UsuariosSectores usec,List<Sroles> RolesAEditar)
        {
            this.id = usec.id;
            this.idSector = usec.idSector;
            this.idUsuario = usec.idUsuario;
            UtilsString Auxiliar = new UtilsString();
            this.roles =Auxiliar.TraducirRolesAString(RolesAEditar);
        }

    }

     

}