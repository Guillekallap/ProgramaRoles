using ProgramaRoles.Models;
using ProgramaRoles.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ProgramaRoles.ViewModels;

namespace ProgramaRoles.Utils
{
    public class UtilsString
    {
        //Primera Parte Del Proyecto

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
            string resultado = string.Join(",", listaRolesSeleccionados.ToArray());
            return resultado;
        }


        //Para la segunda parte del proyecto

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
            List<Roles> listaRoles = UsSecRepo.ListarTodosRoles();
            List<Roles> listaRolesAObtener = new List<Roles>();

            try
            {
                foreach (ViewModel item in lista_VMUsSec)
                {
                    roles.Clear();
                    if (item.roles == null)
                        roles.Add(item.roles);
                    else
                    {
                        roles = item.roles.Split(',').ToList();
                    }
                    //Ver si está en vacío la lista

                    int contador = 0;
                    bool encontrado = false;
                    //Verifico que esté chequeado el rol en el ViewModel paracomprobar si fue modificado o no. 
                    if (item.Chked)
                    {
                        if (roles.Count() == 1 && roles.First() == null)
                        {
                            string rolesArreglado = rolElegido;
                            UsSecRepo.ModificarRolesUsuarioSector(item.Id, rolesArreglado);
                        }
                        else
                        {
                            foreach (string rol in roles)
                            {
                                if (!(rolElegido.Equals(rol)) && (encontrado == false))
                                {
                                    contador++;

                                    if (contador == roles.Count())
                                    {
                                        //Se Añade el Rol elegido a la lista.
                                        listaRolesAObtener.Add(listaRoles.Find(x => x.rol == rolElegido));
                                        //Se Cargan los otros Roles que poseía el UsuarioSector en el string. 
                                        foreach (string rol2 in roles)
                                        {
                                            Roles rolClaseTemp = listaRoles.Find(x => x.rol == rol2);
                                            listaRolesAObtener.Add(rolClaseTemp);
                                        }


                                        listaRolesAObtener = listaRolesAObtener.OrderBy(x => x.id).ToList();
                                        foreach (Roles rolNombre in listaRolesAObtener)
                                        {
                                            rolesOrdenados.Add(rolNombre.rol);
                                        }
                                        string rolesArreglado = string.Join(",", rolesOrdenados.ToArray());
                                        UsSecRepo.ModificarRolesUsuarioSector(item.Id, rolesArreglado);
                                        listaRolesAObtener.Clear();
                                        rolesOrdenados.Clear();
                                    }
                                }
                                else
                                {
                                    encontrado = true;
                                }

                            }
                        }
                    }
                    //Verifico que esté chequeado el rol en el ViewModel para comprobar si fue modificado o no. 
                    else
                    {
                        foreach (string rol in roles)
                        {
                            if ((rolElegido.Equals(rol)))
                            {
                                foreach (string rol2 in roles)
                                {
                                    Roles rolClase = listaRoles.Find(x => x.rol == rol2);
                                    listaRolesAObtener.Add(rolClase);
                                }
                                listaRolesAObtener.Remove(listaRoles.Find(x => x.rol == rolElegido));
                                listaRolesAObtener = listaRolesAObtener.OrderBy(x => x.id).ToList();
                                foreach (Roles rolNombre in listaRolesAObtener)
                                {
                                    rolesOrdenados.Add(rolNombre.rol);
                                }
                                string rolesArreglado = string.Join(",", rolesOrdenados.ToArray());
                                if (rolesArreglado == "") { rolesArreglado = null; }
                                UsSecRepo.ModificarRolesUsuarioSector(item.Id, rolesArreglado);
                                listaRolesAObtener.Clear();
                                rolesOrdenados.Clear();
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //Para tercera parte del proyecto

        //Aplicar la comparacion de string roles que tenia el usuarioSector y los nuevos que acaba de ingresar por la view para tener los roles temporales en un campo directo hacia UsuarioRolHorario
        public string OrdenarListaDeRolesTemporales(string roles1, List<string> roles2)
        {

            List<string> listaRoles1 = new List<string>();
            List<string> rolesFinales = new List<string>();

            if (roles1 != null)
            {
                listaRoles1 = roles1.Split(',').ToList();
            }

            if(listaRoles1.Count()==1 && listaRoles1.First() == "" && roles2.Count()!=0)
            {
                return string.Join(",", roles2.ToArray()); //Roles iguales sin modificar
            }

            if (roles2.Count() == 0)
            {
                if (roles1 == "")
                {
                    return null;
                }
                return string.Join(",", listaRoles1.ToArray()); //Roles iguales sin modificar
            }
            else
            {
                foreach (var rol2 in roles2)
                {
                     bool encontrado = listaRoles1.Contains(rol2);
                     if (encontrado == false)
                     {
                        rolesFinales.Add(rol2);
                     }
                }
                foreach (var rol1 in listaRoles1)
                {
                    bool encontrado = roles2.Contains(rol1);
                    if (encontrado == false)
                    {
                        rolesFinales.Add(rol1);
                    }
                }             
            }
            string rolesArreglado = string.Join(",", rolesFinales.ToArray());
            if (rolesArreglado == ""){ return null; }
            return rolesArreglado;
        }

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
