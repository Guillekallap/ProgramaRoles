using ProgramaRoles.Models;
using ProgramaRoles.Repository;
using ProgramaRoles.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramaRoles.Controllers
{
    public class UsuariosSectoresController : Controller
    {
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
            UsSecRepository UsSecRepo = new UsSecRepository();
            List<ViewModel> lista = new List<ViewModel>();

            ModelState.Clear();

            List<UsuariosSectores> aux = UsSecRepo.ListarTodosUsuariosSectores(dni, nombreUsuario, nombreSector);
            
            foreach (var item in aux)
            {
                ViewModel vModel = new ViewModel(item);
                lista.Add(vModel);
            }
            return PartialView("_PartialView", lista);
        }

        [HttpPost]
        public ActionResult ControladorPartialView(List<ViewModel> lista_users)
        {
                    List<UsuariosSectores> usec = new List<UsuariosSectores>();
                    UsSecRepository UsSecRepo = new UsSecRepository();
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
            var viewModelUsMuestra = new ViewModelUsuarioMuestra
            {
                listaUsuarioRol = (List<ViewModelUsuarioRol>)TempData["listaSeleccion"],
                listaUsuarioRolHorario = new List<ViewModelUsuarioRolHorario>()
            };

            foreach (var item in viewModelUsMuestra.listaUsuarioRol)
            {
                //Logica de los roles Anteriores
                ViewModelUsuarioRolHorario VMUsRolHorario = new ViewModelUsuarioRolHorario(item);
                viewModelUsMuestra.listaUsuarioRolHorario.Add(VMUsRolHorario);
            }
            return View(viewModelUsMuestra);
        }

        [HttpPost]
        public ActionResult EditarRoles(ViewModelUsuarioMuestra listaTotal)
        {
            UsSecRepository UsSecRepo = new UsSecRepository();
            UtilsString utils = new UtilsString();
            List <UsuarioRolHorario> listaUsRolHorario = null;
            try
            {

                foreach (var item in listaTotal.listaUsuarioRolHorario)
               {
                    if(item.Chked == true)
                    {
                        List<DateTime> listaDeFechas = utils.conversionStringAFecha(item.fechas, item.horas);
                        //Logica de los roles nuevos                   
                        List<Sroles> rolesAEditar = listaTotal.listaUsuarioRol.First(x => x.id == item.idUsuarioSector).nombreRoles;
                        item.rolesNuevos = utils.TraducirRolesAString(rolesAEditar);

                        //Logica de las fechas y hora por cada usuarioSector(listaUsuarioRolHorario)
                        while (listaDeFechas.Count>1)
                        {
                            item.fechaInicio = listaDeFechas.First();
                            listaDeFechas.RemoveAt(0);
                            item.fechaFin = listaDeFechas.First();
                            listaDeFechas.RemoveAt(0);
                            UsuarioRolHorario usRolHorario = new UsuarioRolHorario(item);
                            listaUsRolHorario.Add(usRolHorario);
                        }
                        //if(item.emailChked == true)
                        //{
                        //    //Logica del email
                        //    (new UtilsEmail()).EnviarEmail(item.email,item.nombreUsuario,item.rolesNuevos);
                        //}
                    }
                    else
                    {
                        List<Sroles> rolesAEditar = listaTotal.listaUsuarioRol.First(x => x.id == item.idUsuarioSector).nombreRoles;
                        string roles = utils.TraducirRolesAString(rolesAEditar);
                        UsSecRepo.ModificarRolesUsuarioSector(item.idUsuarioSector, roles);

                    }
               }
                if(listaUsRolHorario != null)
                {
                    foreach(var usRolHorario in listaUsRolHorario)
                    {
                        UsSecRepo.AgregarUsuarioSectorRolHorario(usRolHorario.idUsuarioSector, usRolHorario.nombreUsuario, usRolHorario.rolesAnteriores, usRolHorario.rolesNuevos, usRolHorario.email, usRolHorario.emailChked, usRolHorario.fechaInicio, usRolHorario.fechaFin,usRolHorario.fechaActual,usRolHorario.vigente);
                    }

                }
               return RedirectToAction("ObtenerUsuariosSectores");

            }
            catch (Exception)
            {
                return RedirectToAction("ObtenerUsuariosSectores");
            }

        }

    }
}
