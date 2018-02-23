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
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerRoles(string descripcion)
        {
            string descrip = null;
            if (descripcion != null)
            {
                descrip = descripcion;
            }
            else
            {
                return View("ObtenerUsuariosSectores");
            }


            UsSecRepository usSecRepo = new UsSecRepository();
            UtilsString utils = new UtilsString();
            List<Roles> listadoDeRoles = usSecRepo.ListarTodosRoles();
            List<string> listadoDeNombreRoles = utils.ConvertirDeListaDeRolesAListaNombreRoles(listadoDeRoles);
            return View("_PartialView");
        }


//        string nomsec = null;
//        string nomusu = null;
//        string dni2 = null;
//            if (dni != null && dni.Length <= 8)
//            {
//                dni2 = dni;
//            }
//            else
//            {
//                return View("ObtenerUsuariosSectores");
//}

//            if (nombreUsuario != null && nombreUsuario.Length <= 255)
//            {
//                nomusu = nombreUsuario;
//            }
//            else
//            {
//                return View("ObtenerUsuariosSectores");
//            }
//            if (nombreSector != null && nombreSector.Length <= 100)
//            {
//                nomsec = nombreSector;
//            }
//            else
//            {
//                return View("ObtenerUsuariosSectores");
//            }


//            TempData["dni"] = dni2;
//            TempData["nombreUsuario"] = nomusu;
//            TempData["nombreSector"] = nomsec;
//            return View();
//        }


//        public ActionResult ControladorPartialView1()
//{
//    string dni = string.Empty;
//    string nombreUsuario = string.Empty;
//    string nombreSector = string.Empty;
//    if (TempData.Keys.Contains("dni"))
//    {
//        dni = TempData["dni"].ToString();
//        nombreUsuario = TempData["nombreUsuario"].ToString();
//        nombreSector = TempData["nombreSector"].ToString();
//    }
//    UsSecRepository UsSecRepo = new UsSecRepository();
//    List<ViewModel> lista = new List<ViewModel>();

//    ModelState.Clear();

//    List<UsuariosSectores> aux = UsSecRepo.ListarTodosUsuariosSectores(dni, nombreUsuario, nombreSector);

//    foreach (var item in aux)
//    {
//        ViewModel vModel = new ViewModel(item);
//        lista.Add(vModel);
//    }
//    return PartialView("_PartialView", lista);
//}

    }
}
