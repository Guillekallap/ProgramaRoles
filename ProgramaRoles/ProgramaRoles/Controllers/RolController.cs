using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgramaRoles.Models;
using ProgramaRoles.Utils;
using ProgramaRoles.Repository;


namespace ProgramaRoles.Controllers
{
    public class RolController : Controller
    {
        UsSecRepository UsSecRepo = new UsSecRepository();

        public ActionResult ObtenerRoles()
        {
            UtilsString utils = new UtilsString();
            List<Roles> listadoDeRoles = UsSecRepo.ListarTodosRoles();
            List<Sroles> listadoDeSRoles = new List<Sroles>();
            foreach (Roles rol in listadoDeRoles)
            {
                Sroles rolSel = new Sroles(rol,false);
                listadoDeSRoles.Add(rolSel);
            }

            return View(listadoDeSRoles);
        }

        [HttpPost]
        public ActionResult ObtenerRoles(List<Sroles> listadoDeRoles)
        {
            List<ViewModel> listaCheckEnUsuarioSector = new List<ViewModel>();
            Roles rolSeleccionado = new Roles();
            foreach (Sroles item in listadoDeRoles)
            {
                if (item.RolSeleccionado == true)
                {
                    rolSeleccionado = item.roles;
                }
            }
            TempData["rolSeleccionado"] = rolSeleccionado;

            return RedirectToAction("EditarUsuarioSectorFiltrado");
        }

        public ActionResult EditarUsuarioSectorFiltrado(string dni, string nombreUsuario, string nombreSector)
        {
            string nomsec = null;
            string nomusu = null;
            string dni2 = null;
            Roles rolSeleccionado = (Roles)TempData["rolSeleccionado"];
            List<ViewModel> lista_VMUsSec = new List<ViewModel>();

            if (dni != null && dni.Length <= 8)
            {
                dni2 = dni;
            }
            else
            {
                return View("EditarUsuarioSectorFiltrado");
            }

            if (nombreUsuario != null && nombreUsuario.Length <= 255)
            {
                nomusu = nombreUsuario;
            }
            else
            {
                return View("EditarUsuarioSectorFiltrado");
            }
            if (nombreSector != null && nombreSector.Length <= 100)
            {
                nomsec = nombreSector;
            }
            else
            {
                return View("EditarUsuarioSectorFiltrado");
            }

            List<UsuariosSectores> lista_aux = UsSecRepo.ListarTodosUsuariosSectores(dni2, nomusu, nomsec);
            foreach (var item in lista_aux)
            {
                ViewModel vModel = new ViewModel(item, rolSeleccionado.rol);
                lista_VMUsSec.Add(vModel);
            }
            return View(lista_VMUsSec);
        }

        [HttpPost]
        public ActionResult EditarUsuarioSectorFiltrado(List<ViewModel> lista_VMUsSec)
        {
            List<UsuariosSectores> listaUsuarioSector = new List<UsuariosSectores>(); 
            foreach (ViewModel item in lista_VMUsSec)
            {
                if (item.Chked)
                {
                    var i = item.Id;
                    listaUsuarioSector.Add(UsSecRepo.BuscarUsuarioSector(i));
                }
                else
                {
                    //Falta Implementar
                }
            }
            return View("EditarUsuarioSectorFiltrado");
        }

    }
}
