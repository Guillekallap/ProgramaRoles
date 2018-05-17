using ProgramaRoles.Models;
using ProgramaRoles.Repository;
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
        //Modificar Estructura
        [HttpPost]
        public ActionResult EditarRoles(FormCollection collection)
        {
            try
            {
                int cantidadDeParametros = (int)TempData["cantidadUser"];
                List<string> listaRoles = (List<string>)TempData["listaRoles"];
                List<string> listaNombreUsuario = (List<string>)TempData["listaNombreUsuarios"];
                List<string> listaEmail = (List<string>)TempData["listaEmails"];

                List<Roles> listaClaseRoles = UsSecRepo.ListarTodosRoles();
                int cantRoles = listaClaseRoles.Count();
                List<UsuarioRolHorario> listaUsuarioRolhorario = new List<UsuarioRolHorario>();

                for (int i = 0; i < cantidadDeParametros; i++)
                {
                    UsuariosSectores user = new UsuariosSectores();
                    UsuarioRolHorario userHora = new UsuarioRolHorario();
                    List<string> listaRolesString = new List<string>();

                    string fecha = collection["fechaString_" + i].ToString();
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
                    string rolesNuevos = (new UtilsString()).OrdenarRolesPorID(listaClaseRoles, listaRolesString);

                    //Resoluci�n y guardado  a partir de la fecha.
                    if (fecha == "")
                    {
                        //Guardado como siempre sin fechas.
                        UsSecRepo.ModificarRolesUsuarioSector(user.id, rolesNuevos);

                        //Borra lo que no necesita para designarlo correctamente a UsuarioRolHorario
                        listaRoles.RemoveAt(0);
                        listaNombreUsuario.RemoveAt(0);
                        listaEmail.RemoveAt(0);
                    }
                    else
                    {

                        //Realizar lo de UsuarioRolHorario
                        if (collection["emailChked_" + i].Contains("true")) { userHora.emailChked = true; }
                        else { userHora.emailChked = false; }

                        //Obtener roles "anteriores" a partir de la vista.
                        user.roles = listaRoles.First();
                        listaRoles.RemoveAt(0);
                        userHora.nombreUsuario = listaNombreUsuario.First();
                        listaNombreUsuario.RemoveAt(0);
                        userHora.email = listaEmail.First();
                        listaEmail.RemoveAt(0);

                        //Ya tengo en listaRolesString todos los que fue eligiendo el usuario.
                        //Como resultado en rolesTemporales obtengo aquellos que no se agregaron.
                        userHora.rolesTemporales = (new UtilsString()).OrdenarListaDeRolesTemporales(user.roles, listaRolesString);

                        //Parte de Fechas

                        List<DateTime> listaFechas = (new UtilsString()).conversionStringAFecha(fecha);
                        listaFechas = (new UtilsString()).identificarFechaInicioFechaFin(listaFechas);

                        while (listaFechas.Count() > 0)//1
                        {
                            userHora.fechaInicio = listaFechas.First();
                            listaFechas.RemoveAt(0);
                            userHora.fechaFin = listaFechas.First();
                            listaFechas.RemoveAt(0);
                            UsuarioRolHorario usRolHorario = new UsuarioRolHorario(user.id, userHora.nombreUsuario, userHora.rolesTemporales, userHora.email, userHora.fechaInicio, userHora.fechaFin, userHora.emailChked);
                            listaUsuarioRolhorario.Add(usRolHorario);
                        }
                    }
                }


                //Tramitar los repetidos

                if (listaUsuarioRolhorario != null)
                {
                    List<DateTime> listaFechasUSRH = (new UtilsString()).listadoDeFechasPorUsuarioRolHorario(listaUsuarioRolhorario);
                    foreach (var usRolHorario in listaUsuarioRolhorario)
                    {
                        int cantVecesRepetido = 0;
                        //Reveer m�s adelante
                        List<UsuarioRolHorario> miniListaUsRoHorario = UsSecRepo.ListarUsuarioRolHorario(usRolHorario.idUsuarioSector, usRolHorario.fechaInicio, usRolHorario.fechaFin);
                        foreach (var user in miniListaUsRoHorario)
                        {
                            if (!(((usRolHorario.fechaInicio.CompareTo(user.fechaInicio) < 1) && (usRolHorario.fechaFin.CompareTo(user.fechaInicio) < 1)) && ((usRolHorario.fechaInicio.CompareTo(user.fechaFin) < 1) && (usRolHorario.fechaFin.CompareTo(user.fechaFin) < 1)) || ((usRolHorario.fechaInicio.CompareTo(user.fechaInicio) > -1) && (usRolHorario.fechaFin.CompareTo(user.fechaInicio) > -1)) && ((usRolHorario.fechaInicio.CompareTo(user.fechaFin) > -1) && (usRolHorario.fechaFin.CompareTo(user.fechaFin) > -1))))
                            {
                                cantVecesRepetido++;
                            }
                            if (cantVecesRepetido != 0) { break; }
                        }

                        //Se puede buscar una manera eficiente de informar al usuario
                        //Como decir este UsuarioRolHorario no puede ser ingresado a la bdd o un alert('Los UsuarioRolHorario que no han sido ingresados son los siguientes: "mostrar id y fechas."')
                        if (cantVecesRepetido == 0)
                        {
                            UsSecRepo.AgregarUsuarioSectorRolHorario(usRolHorario.idUsuarioSector, usRolHorario.nombreUsuario, usRolHorario.rolesTemporales, usRolHorario.email, usRolHorario.emailChked, usRolHorario.fechaInicio, usRolHorario.fechaFin, usRolHorario.fechaModificacion, usRolHorario.vigente);
                        }
                        else
                        {
                            //Agregar un alert o algo parecido.
                            Response.Write("bolaaa");
                            //if(Response.Buffer == true){
                            //    Response.RedirectToRoute("~/UsuariosSectores/ObtenerUsuariosSectores");
                            //}
                            //ModelState.AddModelError("cursos", "Debe seleccionar por lo menos un curso");

                            //return View("ObtenerUsuariosSectores");
                        }
                    }
                }
                return RedirectToAction("ObtenerUsuariosSectores");
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

    }
}


//UtilsString utils = new UtilsString();
//List <UsuarioRolHorario> listaUsRolHorario = null;
//try
//{
//    foreach (var item in listaTotal.listaUsuarioRolHorario)
//   {
//        if( == )
//        {
//            //Redundante
//            item.listaFechas = utils.conversionStringAFecha(item.fechas);
//            item.listaFechas = utils.identificarFechaInicioFechaFin(item.listaFechas);
//            //Logica de los roles nuevos                   
//            List<Sroles> rolesAEditar = listaTotal.listaUsuarioRol.First(x => x.id == item.idUsuarioSector).nombreRoles;
//            string roles = UsSecRepo.BuscarUsuarioSector(item.idUsuarioSector).roles;
//            item.rolesTemporales = utils.OrdenarListaDeRolesTemporales(roles, rolesAEditar);

//            while (item.listaFechas.Count > 0)//1
//            {
//                item.fechaInicio = item.listaFechas.First();
//                item.listaFechas.RemoveAt(0);
//                item.fechaFin = item.listaFechas.First();
//                item.listaFechas.RemoveAt(0);
//                UsuarioRolHorario usRolHorario = new UsuarioRolHorario(item);
//                listaUsRolHorario.Add(usRolHorario);
//            }
//            //Validacion de fechas cruzadas.
//        }
//        else
//        {
//            List<Sroles> rolesAEditar = listaTotal.listaUsuarioRol.First(x => x.id == item.idUsuarioSector).nombreRoles;
//            string roles = utils.TraducirRolesAString(rolesAEditar);
//            UsSecRepo.ModificarRolesUsuarioSector(item.idUsuarioSector, roles);

//        }
//   }
//    if(listaUsRolHorario != null)
//    {
//        foreach(var usRolHorario in listaUsRolHorario)
//        {
//            int cantVecesRepetido = 0;
//            //Reveer m�s adelante
//            List<UsuarioRolHorario> miniListaUsRoHorario = UsSecRepo.ListarUsuarioRolHorario(usRolHorario.idUsuarioSector, usRolHorario.fechaInicio, usRolHorario.fechaFin);
//            foreach (var user in miniListaUsRoHorario)
//            {
//                if (!(((usRolHorario.fechaInicio.CompareTo(user.fechaInicio) < 1) && (usRolHorario.fechaFin.CompareTo(user.fechaInicio) < 1)) && ((usRolHorario.fechaInicio.CompareTo(user.fechaFin) < 1) && (usRolHorario.fechaFin.CompareTo(user.fechaFin) < 1)) || ((usRolHorario.fechaInicio.CompareTo(user.fechaInicio) > -1) && (usRolHorario.fechaFin.CompareTo(user.fechaInicio)  >-1)) && ((usRolHorario.fechaInicio.CompareTo(user.fechaFin) > -1) && (usRolHorario.fechaFin.CompareTo(user.fechaFin) > -1))))
//                {
//                    cantVecesRepetido++;
//                }
//                if (cantVecesRepetido != 0) break;

//            }

//            //Se puede buscar una manera eficiente de informar al usuario
//            //Como decir este UsuarioRolHorario no puede ser ingresado a la bdd o un alert('Los UsuarioRolHorario que no han sido ingresados son los siguientes: "mostrar id y fechas."')
//            if (cantVecesRepetido == 0) {
//                UsSecRepo.AgregarUsuarioSectorRolHorario(usRolHorario.idUsuarioSector, usRolHorario.nombreUsuario, usRolHorario.rolesTemporales, usRolHorario.email, usRolHorario.emailChked, usRolHorario.fechaInicio, usRolHorario.fechaFin, usRolHorario.fechaModificacion, usRolHorario.vigente);
//            }else{
//                return View("EditarRoles");
//            }
//        }

//    }
//return RedirectToAction("ObtenerUsuariosSectores");

//}
//catch (Exception)
//{
//    return RedirectToAction("ObtenerUsuariosSectores");
//}

//        }

//    }
//}
