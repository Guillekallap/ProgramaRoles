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

        public ActionResult ControladorPartialViewRolFiltrado(List<Roles> listadoDeRoles)
        {
            string rol_elegido = null;
            foreach (Roles item in listadoDeRoles)
            {
                rol_elegido = item.rol;
            }

            UsSecRepository UsSecRepo = new UsSecRepository();
            List<UsuariosSectores> listaUsuarioSector = UsSecRepo.ListarTodosUsuariosSectores(null,null,null);
            return View("_PartialViewRolFiltrado", new { datoLista = listaUsuarioSector, datoRol = rol_elegido});
        }
        [HttpPost]
        public ActionResult ControladorPartialViewRolFiltrado(List<UsuariosSectores> listaUsuarioSectorEnviados)
        {
            List<UsuariosSectores> usec = new List<UsuariosSectores>();
            UsSecRepository UsSecRepo = new UsSecRepository();          
            return RedirectToAction("ObtenerUsuariosSectores");
        }

    }
}
