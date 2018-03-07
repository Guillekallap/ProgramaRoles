using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgramaRoles.Models;
using ProgramaRoles.Utils;
using ProgramaRoles.Repository;
using System.Text.RegularExpressions;


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
                Sroles rolSel = new Sroles(rol, false);
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
            ViewBag.listadoRoles = UsSecRepo.ListarTodosRoles();

            return RedirectToAction("EditarUsuarioSector","Rol",new { rolSeleccionado.rol });
        }


        public ActionResult PasarFiltros(string rrol, string ddatosUsuarios)
        {
            var url = Url.Action("EditarUsuarioSector", "Rol", new { datosUsuarios = ddatosUsuarios, rol=rrol});
            return Redirect(url);
        }

        public ActionResult EditarUsuarioSector(string datosUsuarios, string rol)
        {
            ViewBag.rol = rol;
            ViewBag.listadoRoles = UsSecRepo.ListarTodosRoles();
            string nomsec = null;
            string nomusu = null;
            string dni = null;
            string[] datos;
            string[] datosUsu;
            List<string> datosUUsu=new List<string>();
            List<ViewModel> lista_VMUsSec = new List<ViewModel>();

            if (datosUsuarios != null)
            {
                datos = datosUsuarios.Split(' ');
                foreach (var i in datos)
                {
                    datosUsu = i.Split('=');
                    foreach (var j in datosUsu)
                        datosUUsu.Add(j);
                }

                if (datosUUsu[1].Length <= 8)       /* DNI */
                    dni = datosUUsu[1];

                if (datosUUsu[3].Length <= 255)     /* USUARIO */
                    nomusu = datosUUsu[3];

                if (datosUUsu[5].Length <= 100)     /* SECTOR */
                    nomsec = datosUUsu[5];
            }

            List<UsuariosSectores> lista_aux = UsSecRepo.ListarTodosUsuariosSectores(dni, nomusu, nomsec);
            foreach (var item in lista_aux)
            {
                ViewModel vModel = new ViewModel(item, rol);
                lista_VMUsSec.Add(vModel);
            }
            return View(lista_VMUsSec);
        }

        [HttpPost]
        public ActionResult EditarUsuarioSector(List<ViewModel> lista_VMUsSec, string rolElegido)
        {
            UtilsString utils = new UtilsString();
            utils.ModificarDatosRolSegunChequeos(lista_VMUsSec,rolElegido);
            return RedirectToAction("Index","UsuariosSectores");
        }

    }
}
