using ProgramaRoles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ProgramaRoles.Repository;
using System.Net.Mail;
using System.Net;

namespace ProgramaRoles.Utils
{
    public class UtilsFecha
    {
        //Para tercera parte del proyecto

        public bool VerificarFechaVigenciaDeRol(DateTime fechaInicio)
        {
            return fechaInicio.CompareTo(DateTime.Now) < 1 ? true : false;
        }

        public static bool VerificarFechaMenorALaActual(DateTime fecha)
        {
            return fecha < DateTime.Now ? true : false;
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
            List<DateTime> fechasInicioFin = new List<DateTime>();

            //Buscar la fecha actual
            DateTime fechaActual = listaFechas.Find(x => (x.Date == DateTime.Now.Date) && (x.Month == DateTime.Now.Month) && (x.Year == DateTime.Now.Year));

            if (listaFechas.Count() == 1 && listaFechas.First().Day == DateTime.Now.Day && listaFechas.First().Month == DateTime.Now.Month && listaFechas.First().Year == DateTime.Now.Year)
            {
                fechasInicioFin.Add(listaFechas.First());
                fechasInicioFin.Add(listaFechas.First().AddHours(23).AddMinutes(59).AddSeconds(59));
                return fechasInicioFin;
            }

            //Eliminar Fechas Menores a la actual
            if (listaFechas != null)
            {
                listaFechas.RemoveAll(VerificarFechaMenorALaActual);
                listaFechas = listaFechas.OrderBy(x => x.Date).ToList();
                if (listaFechas.Count() == 0)
                {
                    return fechasInicioFin;
                }
                if (listaFechas.Count() == 1 && fechaActual == DateTime.MinValue)
                {
                    fechasInicioFin.Add(listaFechas.First());
                    fechasInicioFin.Add(listaFechas.First().AddHours(23).AddMinutes(59).AddSeconds(59));
                    return fechasInicioFin;
                }
            }

            //Agrego la primera fecha para tener de referencia en la lista.    
            if (fechaActual != DateTime.MinValue)
            {
                listaFechas.Insert(0, fechaActual);
            }
            DateTime primerFecha = listaFechas.First();
            fechasInicioFin.Add(primerFecha);

            foreach (var fecha in listaFechas)
            {
                if (primerFecha.AddDays(1) == fecha && fechaSiguiente)
                {
                    contadorDias++;
                    fechaSiguiente = true;
                    if (contadorDias == 1 && primerFecha != fechasInicioFin.First())//Para obtener la fecha de inicio del rango de fechas
                    {
                        fechasInicioFin.Add(primerFecha);
                    }
                    primerFecha = fecha;
                }
                else
                {

                    if (contadorDias == 0 && fechaSiguiente)
                    {
                        if (listaFechas.First() == primerFecha)
                        {
                            fechasInicioFin.Add(primerFecha.AddHours(23).AddMinutes(59).AddSeconds(59));//En el caso de que una fecha dure solamente un dia, se guarda la Fecha Fin de la misma
                        }
                        else
                        {
                            fechasInicioFin.Add(primerFecha);//En el caso de que una fecha dure solamente un dia, se guarda la Fecha Inicio de la misma
                        }

                        if (primerFecha.AddDays(1) != fecha && primerFecha != fechasInicioFin.First())
                        {
                            fechasInicioFin.Add(primerFecha.AddHours(23).AddMinutes(59).AddSeconds(59));
                        }
                        primerFecha = fecha;//Obtengo el valor de la fecha actual para trabajarlo en la siguiente fecha
                    }
                    if (contadorDias != 0)
                    {

                        fechasInicioFin.Add(fechasInicioFin.Last().AddDays(contadorDias).AddHours(23).AddMinutes(59).AddSeconds(59));//Fecha Fin del rango
                        primerFecha = fecha;//Obtengo el valor de la fecha actual para trabajarlo en la siguiente fecha
                        contadorDias = 0;

                    }
                }

                if (fecha == listaFechas.Last())//Si es el ultimo de la lista de fechas
                {
                    if (contadorDias != 0)
                    {
                        fechasInicioFin.Add(fecha.AddHours(23).AddMinutes(59).AddSeconds(59));//Agrego el fin de un rango
                    }
                    else
                    {
                        fechasInicioFin.Add(fecha);//Agrego Fecha inicio de una fecha sola
                    }
                }
                fechaSiguiente = true;
            }

            if (fechasInicioFin.Count() % 2 == 1)//Verifico que en el caso de que la lista sea impar se agrega la fecha fin de la misma.
            {
                DateTime aux = fechasInicioFin.Last();
                fechasInicioFin.Add(aux.AddHours(23).AddMinutes(59).AddSeconds(59));
            }

            return fechasInicioFin;
        }

        public void listadoDeFechasPorUsuarioRolHorario(List<UsuarioRolHorario> listaUSRH, List<string> fechaRoles, List<DateTime> listaFechas)
        {
            if (listaUSRH.Count()==0)
            {
                listaFechas=null;
                fechaRoles = null;
            }
            else
            {
                UsuariosSectores usuario = (new UsSecRepository()).BuscarUsuarioSector(listaUSRH.First().idUsuarioSector);

                foreach (var entity in listaUSRH)
                {
                    TimeSpan span = entity.fechaFin - entity.fechaInicio;
                    if (span.Days > 0)
                    {
                        for (int i = 0; i < (span.Days + 1); i++)
                        {
                            listaFechas.Add(entity.fechaInicio.AddDays(i));
                            string rolEntity = (new UtilsRoles()).comprobarRoles(usuario, entity);
                            fechaRoles.Add(rolEntity);
                        }
                    }
                    else
                    {
                        listaFechas.Add(entity.fechaInicio);
                        string rolEntity = (new UtilsRoles()).comprobarRoles(usuario, entity);
                        fechaRoles.Add(rolEntity);
                    }
                }
                listaFechas = listaFechas.Select(x => x.Date).Distinct().Where(x => ((x.Date > DateTime.Now) || ((x.Date.Day == DateTime.Now.Day) && (x.Date.Month == DateTime.Now.Month) && (x.Date.Year == DateTime.Now.Year)))).OrderBy(x => x.Date).ToList();

            }

        }

        public List<UsuarioRolHorario> AcortarFechas(List<UsuarioRolHorario> ListUSRH, DateTime fechaInicio, DateTime fechaFin)
        {
            UsSecRepository usSecRepo = new UsSecRepository();
            TimeSpan fechaRef = new TimeSpan(23, 59, 59);
            TimeSpan fechaRef1 = new TimeSpan(0, 0, 1);

            //Arreglar ver para cortar las fechas segun lo que se quiere obtener.
            List<UsuarioRolHorario> listadoActualizados = new List<UsuarioRolHorario>();

            foreach (var USRH in ListUSRH)
            {
                //Verifico que si las fechas son iguales se borra toda la tupla.
                if (USRH.fechaInicio == fechaInicio && USRH.fechaFin == fechaFin)
                {
                    usSecRepo.EliminarUsuarioRolHorario(USRH.id);
                }
                else
                {
                    TimeSpan diferenciaInicioInicio = fechaInicio - USRH.fechaInicio;

                    if (diferenciaInicioInicio.Days > 0)
                    {
                        UsuarioRolHorario USRHAux = new UsuarioRolHorario(USRH.idUsuarioSector, USRH.nombreUsuario, USRH.rolesTemporales, USRH.email, USRH.fechaInicio, USRH.fechaFin, USRH.emailChked);
                        USRHAux.fechaFin = fechaInicio.Subtract(fechaRef1);
                        listadoActualizados.Add(USRHAux);
                        diferenciaInicioInicio = USRH.fechaFin - fechaFin;

                        if (diferenciaInicioInicio.Days > 0)
                        {
                            USRH.fechaInicio = fechaFin.Subtract(fechaRef).AddDays(1);
                            listadoActualizados.Add(USRH);
                        }
                        usSecRepo.EliminarUsuarioRolHorario(USRH.id);
                    }
                    else
                    {
                        diferenciaInicioInicio = fechaFin - USRH.fechaFin;
                        if (diferenciaInicioInicio.Days >= 0)
                        {
                            usSecRepo.EliminarUsuarioRolHorario(USRH.id);
                        }
                        else
                        {
                            USRH.fechaInicio = fechaFin.Subtract(fechaRef).AddDays(1);
                            listadoActualizados.Add(USRH);

                            usSecRepo.EliminarUsuarioRolHorario(USRH.id);
                        }
                    }
                }
            }

            listadoActualizados.RemoveAll(x => x.fechaFin < DateTime.Now);
            return listadoActualizados;
        }

        public void VerificarFechasAGrabar(List<UsuarioRolHorario> listadoActualizados, List<DateTime> listaFechas)
        {
            TimeSpan fechaRef = new TimeSpan(23, 59, 59);
            TimeSpan fechaRef1 = new TimeSpan(0, 0, 1);
            listaFechas.OrderBy(x => x.Date);
            List<UsuarioRolHorario> listadoActualiza2 = new List<UsuarioRolHorario>();
            List<DateTime>fec = listaFechas;
            foreach (var USRH in listadoActualizados)
            {
                UsuarioRolHorario USRHAux = new UsuarioRolHorario(USRH.idUsuarioSector, USRH.nombreUsuario, USRH.rolesTemporales, USRH.email, USRH.fechaInicio, USRH.fechaFin, USRH.emailChked);

                while(fec.Count() > 2)
                {
                    if (USRH.fechaInicio >= fec.First() && fec.ElementAt(1) <= USRH.fechaFin)
                    {
                        USRHAux.fechaInicio = fec.ElementAt(1).Subtract(fechaRef).AddDays(1);
                        
                        if (USRHAux.fechaInicio <= fec.ElementAt(2) && fec.ElementAt(3) <= USRHAux.fechaFin)
                        {
                            USRHAux.fechaFin = fec.ElementAt(2).Subtract(fechaRef1);
                            listadoActualiza2.Add(USRHAux);

                            USRH.fechaInicio = fec.ElementAt(1).Subtract(fechaRef).AddDays(1);
                            fec.RemoveAt(1);
                        }
                        else
                        {
                            listadoActualiza2.Add(USRHAux);
                            fec.RemoveAt(0);
                        }
                        fec.RemoveAt(0);
                    }
                    else
                    {
                        if (USRH.fechaInicio >= fec.First())
                        {
                            USRHAux.fechaFin = fec.First().Subtract(fechaRef1);
                            listadoActualiza2.Add(USRHAux);

                            USRH.fechaInicio = fec.First();
                        }
                        //Obtengo al inicio que es menor al primer rango de fecha.
                        else
                        {
                            USRHAux.fechaInicio = USRH.fechaInicio;
                            USRHAux.fechaFin = fec.First().Subtract(fechaRef1);
                            listadoActualiza2.Add(USRHAux);

                            USRH.fechaInicio = fec.First();

                        }
                    }
                }

                if (USRH.fechaInicio >= fec.First() && fec.ElementAt(1) <= USRH.fechaFin)
                {
                    USRHAux.fechaInicio = fec.ElementAt(1).Subtract(fechaRef).AddDays(1);
                    listadoActualiza2.Add(USRHAux);
                }
                else
                {
                    if (USRH.fechaInicio >= fec.First())
                    {
                        USRHAux.fechaFin = fec.First().Subtract(fechaRef1);
                        listadoActualiza2.Add(USRHAux);

                        USRH.fechaInicio = fec.First();
                    }
                    //Obtengo al inicio que es menor al primer rango de fecha.
                    else
                    {
                        USRHAux.fechaInicio = USRH.fechaInicio;
                        USRHAux.fechaFin = fec.First().Subtract(fechaRef1);
                        listadoActualiza2.Add(USRHAux);

                        USRH.fechaInicio = fec.First();
                    }
                }
            }
            listadoActualiza2.Distinct();

            //Grabar registros en Base de Datos
            foreach (var Usuario in listadoActualiza2)
            {
                (new UsSecRepository()).AgregarUsuarioSectorRolHorario(Usuario.idUsuarioSector, Usuario.nombreUsuario, Usuario.rolesTemporales, Usuario.email, Usuario.emailChked, Usuario.fechaInicio, Usuario.fechaFin, DateTime.Now, (new UtilsFecha()).VerificarFechaVigenciaDeRol(Usuario.fechaInicio));

            }
        }
    }
}
