using ProgramaRoles.Models;
using ProgramaRoles.Repository;
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
                    List<ViewMuestra> lista = new List<ViewMuestra>();
                    List<Roles> roles = UsSecRepo.ListarTodosRoles();
                    foreach (var item in usec)
                    {
                        ViewMuestra vModel = new ViewMuestra(item, roles);
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
            List<ViewMuestra> listaRecibida = (List<ViewMuestra>)TempData["listaSeleccion"];

            return View(listaRecibida);
        }

        [HttpPost]
        public ActionResult EditarRoles(List<ViewMuestra> listaVMU)
        {
            try{
                List<UsuariosSectores> usec = new List<UsuariosSectores>();
                UsSecRepository UsSecRepo = new UsSecRepository();
                foreach (ViewMuestra item in listaVMU)
                {
                    UsuariosSectores usuariosector = new UsuariosSectores(UsSecRepo.BuscarUsuarioSector(item.id), item.nombreRoles);
                    UsSecRepo.ModificarRolesUsuarioSector(item.id, usuariosector.roles);
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
