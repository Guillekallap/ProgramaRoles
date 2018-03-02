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

            if (listaRolesStringUsuarioSector.Count()==1 && listaRolesStringUsuarioSector.First() =="")
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
        public string TraducirRolesAString(List<Sroles> RolesAEditar)
        {
           
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

        public bool VerificarRolEnUsuarioSector(UsuariosSectores usSec, string rolSeleccionado)
        {
            bool resultado = false;
            List<string> listaRolesUsuarioSector = usSec.roles.Split(',').ToList();
            if (listaRolesUsuarioSector.Count() == 1 && listaRolesUsuarioSector.First() == "")
            {
                resultado = false;
            }
            else
            {
                foreach (string rol in listaRolesUsuarioSector)
                {
                    if (rol.Equals(rolSeleccionado))
                    {
                        resultado = true;
                    }
                }

            }
            return (resultado);
        }

        public void ModificarDatosRolSegunChequeos(List<ViewModel> lista_VMUsSec, string rolElegido)
        {
            UsSecRepository UsSecRepo = new UsSecRepository();
            List<string> roles = new List<string>();
            List<string> rolesOrdenados = new List<string>();

            List<Roles> listaRoles = new List<Roles>();
            foreach (ViewModel item in lista_VMUsSec)
            {
                roles = item.roles.Split(',').ToList();
                //Ver si está en vacío la lista
                int i = item.Id;

                int contador=0;
                //Verifico que esté chequeado el rol en el ViewModel paracomprobar si fue modificado o no. 
                if (item.Chked)
                {
                    if (roles.Count() == 1 && roles.First() == null) {
                        string rolesArreglado = rolElegido;
                        UsuariosSectores usuariosector = new UsuariosSectores(UsSecRepo.BuscarUsuarioSector(i), rolesArreglado);
                        UsSecRepo.ModificarRolesUsuarioSector(usuariosector.id, usuariosector.idSector, usuariosector.idUsuario, usuariosector.roles);
                    }
                    else { 
                        foreach (string rol in roles)
                        {                            
                            if (!(rolElegido.Equals(rol)) && contador!=-1)
                            {
                                contador++;
                                if (contador == roles.Count())
                                {
                                    foreach (string rol2 in roles)
                                    {
                                        Roles rolClase = UsSecRepo.BuscarRol(rol2);
                                        listaRoles.Add(rolClase);
                                    }
                                    listaRoles = listaRoles.OrderBy(x => x.id).ToList();
                                    foreach (Roles rolNombre in listaRoles)
                                    {
                                        //string rolNombreTemporal = rolNombre.rol;
                                        rolesOrdenados.Add(rolNombre.rol);
                                    }
                                    string rolesArreglado = string.Join(",", rolesOrdenados.ToArray());

                                    UsuariosSectores usuariosector = new UsuariosSectores(UsSecRepo.BuscarUsuarioSector(i), rolesArreglado);
                                    UsSecRepo.ModificarRolesUsuarioSector(usuariosector.id, usuariosector.idSector, usuariosector.idUsuario, usuariosector.roles);
                                    listaRoles = null;
                                    rolesOrdenados = null;
                                }
                            }
                            else
                            {
                                contador = -1;
                            }

                        }
                    }
                }
                //Verifico que esté chequeado el rol en el ViewModel paracomprobar si fue modificado o no. 
                else
                {
                    //if (roles.Count() == 1 && roles.First() == "")
                    //{
                    //    string rolesArreglado = "";
                    //    UsuariosSectores usuariosector = new UsuariosSectores(UsSecRepo.BuscarUsuarioSector(i), rolesArreglado);
                    //    UsSecRepo.ModificarRolesUsuarioSector(usuariosector.id, usuariosector.idSector, usuariosector.idUsuario, usuariosector.roles);
                    //}
                    foreach (string rol in roles)
                    {
                       if ((rolElegido.Equals(rol)) && contador!=-1)
                       {
                                contador++;
                                if (contador == roles.Count())
                                {
                                        foreach (string rol2 in roles)
                                        {
                                            Roles rolClase = UsSecRepo.BuscarRol(rol2);
                                            listaRoles.Add(rolClase);
                                        }
                                        listaRoles = listaRoles.OrderBy(x => x.id).ToList();
                                        foreach (Roles rolNombre in listaRoles)
                                        {
                                            //string rolNombreTemporal = rolNombre.rol;
                                            rolesOrdenados.Add(rolNombre.rol);
                                        }
                                        rolesOrdenados.Remove(rol);                        
                                        string rolesArreglado = string.Join(",", rolesOrdenados.ToArray());
                                        UsuariosSectores usuariosector = new UsuariosSectores(UsSecRepo.BuscarUsuarioSector(i), rolesArreglado);
                                        UsSecRepo.ModificarRolesUsuarioSector(usuariosector.id, usuariosector.idSector, usuariosector.idUsuario, usuariosector.roles);
                                 }
                        }
                        else
                        {
                            contador = -1;
                        }

                    }
                }
            }
        }
    }
}
