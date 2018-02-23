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
        public ActionResult ObtenerRoles()
        {
            UsSecRepository usSecRepo = new UsSecRepository();
            UtilsString utils = new UtilsString();
            List<Roles> listadoDeRoles = usSecRepo.ListarTodosRoles();
            List<Sroles> listadoDeSRoles = new List<Sroles>();
            foreach (Roles rol in listadoDeRoles)
            {
                Sroles rolSel = new Sroles(rol,false);
                listadoDeSRoles.Add(rolSel);
            }
            return View(listadoDeSRoles);
        }

        public ActionResult ControladorPartialViewRolFiltrado(List<Sroles> listadoDeRoles)
        {
            List<ViewModel> listaCheckEnUsuarioSector = new List<ViewModel>();
            string rol_elegido = null;


            foreach (Sroles item in listadoDeRoles)
            {
                rol_elegido = item.roles.rol;
            }

            UsSecRepository UsSecRepo = new UsSecRepository();
            List<UsuariosSectores> listaUsuarioSector = UsSecRepo.BuscarUsuarioSectorPorRol(rol_elegido);
            foreach (var item in listaUsuarioSector)
            {
                ViewModel vModel = new ViewModel(item);
                listaCheckEnUsuarioSector.Add(vModel);
            }

            return View("_PartialViewRolFiltrado", new { datoLista = listaCheckEnUsuarioSector, datoRol = rol_elegido});
        }
        [HttpPost]
        public ActionResult ControladorPartialViewRolFiltrado(List<ViewModel> listaUsuarioSectorEnviados)
        {
            List<UsuariosSectores> usec = new List<UsuariosSectores>();
            UsSecRepository UsSecRepo = new UsSecRepository();

            foreach (ViewModel item in listaUsuarioSectorEnviados)
            {
                if (item.Chked)
                {
                    var i = item.Id;
                    usec.Add(UsSecRepo.BuscarUsuarioSector(i));
                }
            }
            return RedirectToAction("ObtenerUsuariosSectores");
        }

    }
}
