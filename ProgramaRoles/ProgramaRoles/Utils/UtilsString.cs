using ProgramaRoles.Models;
using ProgramaRoles.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramaRoles.Utils
{
    public class UtilsString
    {
        public List<string> ConvertirDeListaDeRolesAListaNombreRoles(List<Roles> listaRoles)
        {
            List<string> listaRolesString = new List<string>();

            foreach(var roles in listaRoles)
            {
                listaRolesString.Add(roles.rol);
            }

            return listaRolesString;
        }

        public List<Sroles> ParsearPropiedadRoles( UsuariosSectores usSec, List<string> listaRolesString)
        {

            UsSecRepository UsSecRepo = new UsSecRepository();
            List<string> listaRolesStringUsuarioSector = usSec.roles.Split(',').ToList();
            List<Roles> lista_roles = UsSecRepo.ListarTodosRoles();
            List<Sroles> listadoSRoles = new List<Sroles>();
            List<Sroles> listadoSRolesFalse = new List<Sroles>();

            int i = 0;
            //Parsea la propiedad 'roles' para identificar que roles contiene el UsuarioSector dado. 
            foreach (var rolesUsuarioSector in listaRolesString)
            {
                foreach(var rolStringUsuarioSector in listaRolesStringUsuarioSector ){
                    if (rolesUsuarioSector.Equals(rolStringUsuarioSector))
                    {
                        Sroles seleccionadosRoles = new Sroles();
                        seleccionadosRoles.roles = UsSecRepo.BuscarRol(rolStringUsuarioSector);
                        seleccionadosRoles.RolSeleccionado = true;
                        listadoSRoles.Add(seleccionadosRoles);
                    }
                }
            }

            foreach (var rolesTotales in lista_roles)
            {
                foreach (var rolListadoSRoles in listadoSRoles)
                {
                    if (!rolesTotales.id.Equals(rolListadoSRoles.roles.id))
                    {
                        i++;
                    }
                    if (i == listadoSRoles.Count()) {
                        Sroles seleccionadosRoles = new Sroles();
                        seleccionadosRoles.roles = UsSecRepo.BuscarRol(rolesTotales.rol); ;
                        seleccionadosRoles.RolSeleccionado = false;
                        listadoSRolesFalse.Add(seleccionadosRoles);
                    }
                }
                i = 0;
            }

            listadoSRoles.AddRange(listadoSRolesFalse);
            listadoSRoles = listadoSRoles.OrderBy(x => x.roles.id).ToList();
            return listadoSRoles;

        }
//A partir de la Lista Sroles creo una lista de string con los roles modificados, luego la convierto a string para agregarla como atributo a la propiedad roles de UsuariosSectores.
        public string TraducirRolesAString(List<Sroles> RolesAEditar) {
           
            List<string> listaRolesSeleccionados = new List<string>(); 
            foreach (var item in RolesAEditar)
            {
                if (item.RolSeleccionado == true)
                {
                    listaRolesSeleccionados.Add(item.roles.rol);
                }
            }
            string resultado = string.Join(",",listaRolesSeleccionados.ToArray());
            return resultado;
        }

        public List<string>

    }
}
