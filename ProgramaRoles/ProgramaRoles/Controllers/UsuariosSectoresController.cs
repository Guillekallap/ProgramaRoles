using ProgramaRoles.Models;
using ProgramaRoles.Repository;
using ProgramaRoles.ViewModels;
using ProgramaRoles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProgramaRoles.Controllers
{
    public class UsuariosSectoresController : Controller
    {
        UsSecRepository UsSecRepo = new UsSecRepository();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObtenerUsuariosSectores()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerUsuariosSectores(string dni, string nombreUsuario, string nombreSector)
        {
            string nomsec = null;
            string nomusu = null;
            string dni2 = null;
            if (dni != null && dni.Length <= 8)
            {
                dni2 = dni;
            }
            else
            {
                return View("ObtenerUsuariosSectores");
            }

            if (nombreUsuario != null && nombreUsuario.Length <= 255)
            {
                nomusu = nombreUsuario;
            }
            else
            {
                return View("ObtenerUsuariosSectores");
            }
            if (nombreSector != null && nombreSector.Length <= 100)
            {
                nomsec = nombreSector;
            }
            else
            {
                return View("ObtenerUsuariosSectores");
            }

            TempData["dni"] = dni2;
            TempData["nombreUsuario"] = nomusu;
            TempData["nombreSector"] = nomsec;
            return View();
        }

        public ActionResult ControladorPartialView1()
        {
            string dni = string.Empty;
            string nombreUsuario = string.Empty;
            string nombreSector = string.Empty;
            if (TempData.Keys.Contains("dni"))
            {
                dni = TempData["dni"].ToString();
                nombreUsuario = TempData["nombreUsuario"].ToString();
                nombreSector = TempData["nombreSector"].ToString();
            }
            List<ViewModel> lista = new List<ViewModel>();

            ModelState.Clear();

            List<UsuariosSectores> listaUserSectorAux = UsSecRepo.ListarTodosUsuariosSectores(dni, nombreUsuario, nombreSector);
            
            foreach (var entity in listaUserSectorAux)
            {
                ViewModel vModel = new ViewModel(entity);
                lista.Add(vModel);
            }
            return PartialView("_PartialView", lista);
        }

        [HttpPost]
        public ActionResult ControladorPartialView(List<ViewModel> lista_users)
        {
                    List<UsuariosSectores> usec = new List<UsuariosSectores>();
                    if (lista_users == null)
                    {
                        return RedirectToAction("ObtenerUsuariosSectores", "UsuariosSectores");
                    }
                    foreach (ViewModel item in lista_users)
                    {
                        if (item.Chked)
                        {
                            var i = item.Id;
                            usec.Add(UsSecRepo.BuscarUsuarioSector(i));
                        }
                    }
                    List<ViewModelUsuarioRol> lista = new List<ViewModelUsuarioRol>();
                    List<Roles> roles = UsSecRepo.ListarTodosRoles();
                    foreach (var item in usec)
                    {
                        ViewModelUsuarioRol vModel = new ViewModelUsuarioRol(item, roles);                        
                        lista.Add(vModel);
                    }
                    TempData["listaSeleccion"] = lista;
                    if (lista.Count() == 0)
                    {
                        return RedirectToAction("ObtenerUsuariosSectores", "UsuariosSectores");
                    }
                    return RedirectToAction("EditarRoles", "UsuariosSectores");
        }

        public ActionResult EditarRoles()
        {
            List<string> listaRol = new List<string>();
            List<string> listaNombreUsuario = new List<string>();
            List<string> listaEmail = new List<string>();

            ViewModelUsuarioMuestra viewModelUsMuestra = new ViewModelUsuarioMuestra
            {
                listaUsuarioRol = (List<ViewModelUsuarioRol>)TempData["listaSeleccion"],
                listaUsuarioRolHorario = new List<ViewModelUsuarioRolHorario>()
            };

            foreach (var item in viewModelUsMuestra.listaUsuarioRol)
            {
                //Logica de los roles Anteriores
                ViewModelUsuarioRolHorario VMUsRolHorario = new ViewModelUsuarioRolHorario(item);
                viewModelUsMuestra.listaUsuarioRolHorario.Add(VMUsRolHorario);
                listaRol.Add(item.roles);
                listaNombreUsuario.Add(item.nombreUsuario);
                listaEmail.Add(item.email);
            }

            TempData["cantidadUser"] = viewModelUsMuestra.listaUsuarioRol.Count();
            TempData["listaRoles"] = listaRol;
            TempData["listaNombreUsuarios"] = listaNombreUsuario;
            TempData["listaEmails"] = listaEmail;

            return View(viewModelUsMuestra);
        }

        [HttpPost]
        public ActionResult EditarRoles(FormCollection collection)
        {
            int cantidadDeParametros = (int)TempData["cantidadUser"];
            List<string> listaRoles = (List<string>)TempData["listaRoles"];
            List<string> listaNombreUsuario = (List<string>)TempData["listaNombreUsuarios"];
            List<string> listaEmail = (List<string>)TempData["listaEmails"];

            try
            {                
                List<Roles> listaClaseRoles = UsSecRepo.ListarTodosRoles();
                int cantRoles = listaClaseRoles.Count();
                List<UsuarioRolHorario> listaUSRHAGrabar = new List<UsuarioRolHorario>();
                List<UsuarioRolHorario> listaUSRHAGrabarInvalido = new List<UsuarioRolHorario>();
                List<UsuariosSectores> listUserAGrabar = new List<UsuariosSectores>();

                for (int i = 0; i < cantidadDeParametros; i++)
                {                    
                    UsuariosSectores user = new UsuariosSectores();
                    UsuarioRolHorario userHora = new UsuarioRolHorario();
                    List<string> listaRolesString = new List<string>();

                    //FechaString luego a validar y convertida en listaFechas
                    string fecha = collection["fechaString_" + i].ToString();

                    //Obtener IDUsuarioSector
                    user.id = Convert.ToInt32(collection["listaUsuarioRol[" + i + "].id"].ToString());

                    //Mecanica de los roles
                    for (int j = 0; j < cantRoles; j++)
                    {

                        bool rolSeleccionado = false;
                        if (collection["listaUsuarioRol[" + i + "].nombreRoles[" + j + "].RolSeleccionado"].ToString().Contains("true"))
                        {
                            rolSeleccionado = true;
                        }

                        if (rolSeleccionado == true)
                        {
                            string rol = collection["listaUsuarioRol[" + i + "].nombreRoles[" + j + "].roles.rol"].ToString();
                            listaRolesString.Add(rol);
                        }
                    }

                    //Se encuentran los roles otorgados por los usuarios
                    user.roles = (new UtilsString()).OrdenarRolesPorID(listaClaseRoles, listaRolesString);

                    //Resolucion y Validacion a partir de la fecha.
                    if (fecha == "")
                    {
                        //Luego para Enviar A Vista GrabarValido
                        listUserAGrabar.Add(user);

                        //Borra lo que no necesita para designarlo correctamente a UsuarioRolHorario
                        listaRoles.RemoveAt(0);
                        listaNombreUsuario.RemoveAt(0);
                        listaEmail.RemoveAt(0);
                    }
                    else
                    {
                        //Verifica el emailChecked para UsuarioRolHorario
                        if (collection["emailChked_" + i].Contains("true")) { userHora.emailChked = true; }
                        else { userHora.emailChked = false; }

                        //Obtener roles "anteriores" a partir de la vista.
                        user.roles = listaRoles.First();
                        listaRoles.RemoveAt(0);
                        //Obtener nombreUsuario a partir de la vista.
                        userHora.nombreUsuario = listaNombreUsuario.First();
                        listaNombreUsuario.RemoveAt(0);
                        //Obtener email a partir de la vista.
                        userHora.email = listaEmail.First();
                        listaEmail.RemoveAt(0);

                        /*Ya tengo en listaRolesString todos los que fue eligiendo el usuario.
                          Como resultado en rolesTemporales obtengo aquellos que no se agregaron.*/
                        userHora.rolesTemporales = (new UtilsString()).OrdenarListaDeRolesTemporales(user.roles, listaRolesString);

                        //Parte de Fechas, Primero conversion de string a fecha, luego se busca el inicio y fin.
                        List<DateTime> listaFechas = (new UtilsString()).conversionStringAFecha(fecha);
                        listaFechas = (new UtilsString()).identificarFechaInicioFechaFin(listaFechas);

                        for (int j=0; j<listaFechas.Count();j++)
                        {
                            userHora.fechaInicio = listaFechas.First();
                            listaFechas.RemoveAt(0);
                            userHora.fechaFin = listaFechas.First();
                            listaFechas.RemoveAt(0);

                            UsuarioRolHorario USRH = new UsuarioRolHorario(user.id, userHora.nombreUsuario, userHora.rolesTemporales, userHora.email, userHora.fechaInicio, userHora.fechaFin, userHora.emailChked);

                            List<DateTime> listaFechasUSRH = (new UtilsString()).listadoDeFechasPorUsuarioRolHorario(USRH);

                            int cantVecesRepetido = 0;
                            List<UsuarioRolHorario> miniListaUsRoHorario = UsSecRepo.ListarUsuarioRolHorario(USRH.idUsuarioSector, USRH.fechaInicio, USRH.fechaFin);

                            foreach (var miniUSRH in miniListaUsRoHorario)
                            {
                                if (!(((USRH.fechaInicio.CompareTo(miniUSRH.fechaInicio) < 1) && (USRH.fechaFin.CompareTo(miniUSRH.fechaInicio) < 1)) && ((USRH.fechaInicio.CompareTo(miniUSRH.fechaFin) < 1) && (USRH.fechaFin.CompareTo(miniUSRH.fechaFin) < 1)) || ((USRH.fechaInicio.CompareTo(miniUSRH.fechaInicio) > -1) && (USRH.fechaFin.CompareTo(miniUSRH.fechaInicio) > -1)) && ((USRH.fechaInicio.CompareTo(miniUSRH.fechaFin) > -1) && (USRH.fechaFin.CompareTo(miniUSRH.fechaFin) > -1))))
                                {
                                    cantVecesRepetido++;
                                }
                                if (cantVecesRepetido != 0) { break; }
                            }

                            if (cantVecesRepetido == 0)
                            {
                                listaUSRHAGrabar.Add(USRH);
                            }
                            else
                            {
                                listaUSRHAGrabarInvalido.Add(USRH);
                            }
                        }
                    }
                }

                TempData["listUsuarioSector"] = listUserAGrabar;
                TempData["listUSRHV"] = listaUSRHAGrabar;
                if (listaUSRHAGrabarInvalido.Count()!=0)
                {
                    TempData["listUSRHI"] = listaUSRHAGrabarInvalido;
                    return RedirectToAction("GrabarUserInvalido");
                }
                return RedirectToAction("GrabarUserValido");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult GrabarUserValido()
        {
            List<UsuariosSectores> listUserAGrabar = (List<UsuariosSectores>)TempData["listUsuarioSector"];
            List<UsuarioRolHorario> listaUSRHAGrabar = (List<UsuarioRolHorario>)TempData["listUSRHV"];

            ViewModelUserValido viewModelUserValido = new ViewModelUserValido {
                listaUsuario = new List<ViewModel>(),
                listaUsuarioRolHorario = new List<ViewModelUsuarioRolHorario>()
            };

            //Guardado como siempre sin fechas.
            foreach (UsuariosSectores user  in listUserAGrabar)
            {
                UsSecRepo.ModificarRolesUsuarioSector(user.id, user.roles);
                ViewModel vmUser = new ViewModel(user);
                viewModelUserValido.listaUsuario.Add(vmUser);
            }
            //Guardado USRH validos.
            foreach (UsuarioRolHorario USRH in listaUSRHAGrabar)
            {
                UsSecRepo.AgregarUsuarioSectorRolHorario(USRH.idUsuarioSector, USRH.nombreUsuario, USRH.rolesTemporales, USRH.email, USRH.emailChked, USRH.fechaInicio, USRH.fechaFin, USRH.fechaModificacion, USRH.vigente);
                ViewModelUsuarioRolHorario vmUserHora = new ViewModelUsuarioRolHorario(USRH);
                viewModelUserValido.listaUsuarioRolHorario.Add(vmUserHora);
            }

            return View(viewModelUserValido);
        }

        public ActionResult GrabarUserInvalido()
        {
            List<UsuarioRolHorario> listaUSRHAGrabarInvalido = (List<UsuarioRolHorario>)TempData["listUSRHI"];
            List<UsuariosSectores> listUserAGrabar = (List<UsuariosSectores>)TempData["listUsuarioSector"];
            List<UsuarioRolHorario> listaUSRHAGrabar = (List<UsuarioRolHorario>)TempData["listUSRHV"];

            ViewModelUserValido viewModelUserInvalido = new ViewModelUserValido
            {
                listaUsuario = new List<ViewModel>(),
                listaUsuarioRolHorario = new List<ViewModelUsuarioRolHorario>()
            };

            //foreach (UsuariosSectores user in listUserAGrabar)
            //{
            //    UsSecRepo.ModificarRolesUsuarioSector(user.id, user.roles);
            //    ViewModel vmUser = new ViewModel(user);
            //    viewModelUserInvalido.listaUsuario.Add(vmUser);
            //}


            //Guardado USRH invalidos.
            foreach (UsuarioRolHorario USRH in listaUSRHAGrabarInvalido)
            {
                //UsSecRepo.AgregarUsuarioSectorRolHorario(USRH.idUsuarioSector, USRH.nombreUsuario, USRH.rolesTemporales, USRH.email, USRH.emailChked, USRH.fechaInicio, USRH.fechaFin, USRH.fechaModificacion, USRH.vigente);
                ViewModelUsuarioRolHorario vmUserHora = new ViewModelUsuarioRolHorario(USRH);
                viewModelUserInvalido.listaUsuarioRolHorario.Add(vmUserHora);
            }

            TempData["listUsuarioSector"] = listUserAGrabar;
            TempData["listUSRHV"] = listaUSRHAGrabar;

            return View(viewModelUserInvalido);
        }

        [HttpPost]
        public ActionResult GrabarUserInvalido(ViewModelUserValido USRHInvalido)
        {
            List<UsuariosSectores> listUserAGrabar = (List<UsuariosSectores>)TempData["listUsuarioSector"];
            List<UsuarioRolHorario> listaUSRHAGrabar = (List<UsuarioRolHorario>)TempData["listUSRHV"];
            
            foreach (ViewModelUsuarioRolHorario USRH in USRHInvalido.listaUsuarioRolHorario)
            {
                if (USRH.Chked)
                {
                    UsSecRepo.AgregarUsuarioSectorRolHorario(USRH.idUsuarioSector, USRH.nombreUsuario, USRH.rolesTemporales, USRH.email, USRH.emailChked, USRH.fechaInicio, USRH.fechaFin, USRH.fechaModificacion, USRH.vigente);
                }
            }
            TempData["listUsuarioSector"] = listUserAGrabar;
            TempData["listUSRHV"] = listaUSRHAGrabar;

            return RedirectToAction("GrabarUserValido");
        }
    }
}