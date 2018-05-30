using System.Collections.Generic;
using ProgramaRoles.Utils;



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
        public string email { get; set; }
        public string roles { get; set; }

        public UsuariosSectores(UsuariosSectores usec,List<Sroles> RolesAEditar)
        {
            this.id = usec.id;
            this.idSector = usec.idSector;
            this.idUsuario = usec.idUsuario;
            UtilsString Auxiliar = new UtilsString();
            this.roles =Auxiliar.TraducirRolesAString(RolesAEditar);
        }

        public UsuariosSectores(UsuariosSectores usec,string rolesArreglado)
        {
            this.id = usec.id;
            this.idSector = usec.idSector;
            this.idUsuario = usec.idUsuario;
            this.roles = rolesArreglado;
        }
    }

}