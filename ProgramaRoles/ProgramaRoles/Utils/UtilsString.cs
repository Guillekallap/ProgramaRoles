﻿using ProgramaRoles.Models;
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
        public string OrdenarListaDeRolesTemporales(string roles1, List<Sroles> roles2)
        {
            List<string> listaRoles1 = roles1.Split(',').ToList();
            List<string> listaRoles2 = new List<string>();
            List<string> rolesFinales = null;

            //Reveer
            if (roles2.Count==0)
            {
                listaRoles2 = null;
            }

            foreach (var rol2 in roles2)
            {
                if (rol2.RolSeleccionado == true)
                    listaRoles2.Add(rol2.roles.rol);
            }

            foreach(var rol2 in listaRoles2)
            {
                bool encontrado = listaRoles1.Contains(rol2);
                if (encontrado == false)
                {
                    rolesFinales.Add(rol2);
                }
            }
            string rolesArreglado = string.Join(",", rolesFinales.ToArray());

            return rolesArreglado;
        }

        public bool VerificarFechaVigenciaDeRol(DateTime fechaInicio)
        {
            if(fechaInicio.CompareTo(DateTime.Now)<1)
                return true;
            else
            {
                return false;
            }
        }    

        public List<DateTime> conversionStringAFecha(string fechas)
        {
            List<string> stringFechas = fechas.Split(',').ToList();
            //string[] stringHoras = horas.Split(':', '-', ',');
            //int i = 0;
            List<DateTime> listaDeFechas = new List<DateTime>();
            foreach (string s in stringFechas)
            {
                int j = 0;
                string[] fechaDatos = s.Split('/');
                int dia = Convert.ToInt32(fechaDatos[j]);
                j++;
                int mes = Convert.ToInt32(fechaDatos[j]);
                j++;
                int año = Convert.ToInt32(fechaDatos[j]);

                //int hora = Convert.ToInt32(stringHoras[i]);
                //i++;
                //int minuto = Convert.ToInt32(stringHoras[i]);
                //i++;
                DateTime fecha = new DateTime(año, mes, dia);
                listaDeFechas.Add(fecha);
            }
            return listaDeFechas;
        }

        public List<DateTime> identificarFechaInicioFechaFin(List<DateTime> listaFechas)
        {
            //Logica de las fechas por cada usuarioSector(listaUsuarioRolHorario)
            bool fechaSiguiente = false;
            int contadorDias = 0;
            DateTime primerFecha = listaFechas.First();
            List<DateTime> fechasInicioFin = new List<DateTime>();
            fechasInicioFin.Add(primerFecha);
            foreach (var fecha in listaFechas)
            {
                if (primerFecha.AddDays(1) == fecha && fechaSiguiente)
                {
                    contadorDias++;
                    fechaSiguiente = true;
                    if (contadorDias == 1 && primerFecha!=fechasInicioFin.First())
                    {
                        fechasInicioFin.Add(primerFecha);
                    }
                    primerFecha = fecha;
                }
                else
                {

                    if (contadorDias==0 && fechaSiguiente) {
                        fechasInicioFin.Add(primerFecha);
                        if (primerFecha.AddDays(1) != fecha && primerFecha!=fechasInicioFin.First())
                        {
                            fechasInicioFin.Add(primerFecha);
                        }
                        primerFecha = fecha;
                    }
                    if (contadorDias!=0){
                        
                            fechasInicioFin.Add(fechasInicioFin.Last().AddDays(contadorDias));//Fecha Fin del rango
                            primerFecha = fecha;
                            contadorDias = 0;
                        
                    }
                }

                if (fecha == listaFechas.Last())
                {
                    fechasInicioFin.Add(fecha);//Fecha inicio
                }
                fechaSiguiente = true;
            }

            if (fechasInicioFin.Count() % 2 == 1)
            {
                DateTime aux = fechasInicioFin.Last();
                fechasInicioFin.Add(aux);
            }

            return fechasInicioFin;
        }
    }
}
