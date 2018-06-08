using ProgramaRoles.Models;
using ProgramaRoles.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ProgramaRoles.ViewModels;


namespace ProgramaRoles.Utils
{
    public class UtilsRoles
    {
        //Primera Parte Del Proyecto

        public List<string> ConvertirDeListaDeRolesAListaNombreRoles(List<Roles> listaRoles)
        {
            List<string> listaRolesString = new List<string>();

            foreach (var roles in listaRoles)
            {
                listaRolesString.Add(roles.rol);
            }

            return listaRolesString;
        }

        public List<Sroles> ParsearPropiedadRoles(UsuariosSectores usSec, List<string> listaRolesString)
        {

            UsSecRepository UsSecRepo = new UsSecRepository();
            List<string> listaRolesStringUsuarioSector = usSec.roles.Split(',').ToList();
            List<Roles> lista_roles = UsSecRepo.ListarTodosRoles();
            List<Sroles> listadoSRoles = new List<Sroles>();
            List<Sroles> listadoSRolesFalse = new List<Sroles>();

            if (listaRolesStringUsuarioSector.Count() == 1 && listaRolesStringUsuarioSector.First() == "")
            {
                foreach (var rolGenerico in lista_roles)
                {
                    Sroles seleccionadosRoles = new Sroles();
                    seleccionadosRoles.roles = UsSecRepo.BuscarRol(rolGenerico.rol);
                    seleccionadosRoles.RolSeleccionado = false;
                    listadoSRoles.Add(seleccionadosRoles);
                }

            }
            int i = 0;

            //Parsea la propiedad 'roles' para identificar que roles contiene el UsuarioSector dado. 
            foreach (var rolesUsuarioSector in listaRolesString)
            {
                foreach (var rolStringUsuarioSector in listaRolesStringUsuarioSector)
                {
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
                    if (i == listadoSRoles.Count())
                    {
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

        //Para tercera parte del proyecto

        public string OrdenarRolesPorID(List<Roles> listaClaseRoles, List<string> listaRolesString)
        {

            List<Roles> listaRolesAObtener = new List<Roles>();
            List<string> listaRolesOrdenados = new List<string>();
            //Ordenar Roles dados por el usuario
            foreach (string rolString in listaRolesString)
            {
                Roles rolClaseTemp = listaClaseRoles.Find(x => x.rol == rolString);
                listaRolesAObtener.Add(rolClaseTemp);
            }

            listaRolesAObtener = listaRolesAObtener.OrderBy(x => x.id).ToList();
            foreach (Roles rolNombre in listaRolesAObtener)
            {
                listaRolesOrdenados.Add(rolNombre.rol);
            }
            string rolesArreglado = string.Join(",", listaRolesOrdenados.ToArray());

            return rolesArreglado;
        }

    }
}