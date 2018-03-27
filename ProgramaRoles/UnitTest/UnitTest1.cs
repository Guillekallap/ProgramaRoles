using ProgramaRoles.Controllers;
using ProgramaRoles.Models;
using ProgramaRoles.Repository;
using ProgramaRoles.Utils;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BuscarRol()
        {
            UsSecRepository UsSec = new UsSecRepository();
            Roles roles = UsSec.BuscarRol("ROLE_USUARIOS_ADMINISTRADOR");
            Assert.IsNotNull(roles);
        }
       [TestMethod]

        public void BuscarUsuarioSector()
        {
            UsSecRepository UsSec = new UsSecRepository();
            UsuariosSectores UsuariosSectores = UsSec.BuscarUsuarioSector(3);
            Assert.IsNotNull(UsuariosSectores);
        }

        [TestMethod]
        public void ListarListarTodosUsuariosSectores()
        {
            UsSecRepository UsSec = new UsSecRepository();
            List<UsuariosSectores> Lista_usSec = UsSec.ListarTodosUsuariosSectores(null, "juan","");
            Assert.IsNotNull(Lista_usSec);
        }

        [TestMethod]
        public void ListarTodosRoles()
        {
            UsSecRepository UsSec = new UsSecRepository();
            List<Roles> Lista_Roles=  UsSec.ListarTodosRoles();
            Assert.IsNotNull(Lista_Roles);
        }

        [TestMethod]
        public void ParsearPropiedadRoles()
        {

            UtilsString Utils = new UtilsString();
            UsSecRepository UsSec = new UsSecRepository();
            UsuariosSectores UsuarioSec = UsSec.BuscarUsuarioSector(4);
            List<Roles> Lista_Roles = UsSec.ListarTodosRoles();
            List<string> Lista_string = Utils.ConvertirDeListaDeRolesAListaNombreRoles(Lista_Roles);
            List<Sroles> Lista_SRoles = new List<Sroles>();
            Lista_SRoles = Utils.ParsearPropiedadRoles(UsuarioSec, Lista_string);
            //string StringResultante = Utils.TraducirRolesAString(Lista_SRoles);
            Assert.IsNotNull(Lista_SRoles);
        }

        [TestMethod]
        public void ParsearPropiedadRolesNulo()
        {

            UtilsString Utils = new UtilsString();
            UsSecRepository UsSec = new UsSecRepository();
            UsuariosSectores UsuarioSec = UsSec.BuscarUsuarioSector(2011);
            List<Roles> Lista_Roles = UsSec.ListarTodosRoles();
            List<string> Lista_string = Utils.ConvertirDeListaDeRolesAListaNombreRoles(Lista_Roles);
            List<Sroles> Lista_SRoles = new List<Sroles>();
            Lista_SRoles = Utils.ParsearPropiedadRoles(UsuarioSec, Lista_string);
            //string StringResultante = Utils.TraducirRolesAString(Lista_SRoles);
            Assert.IsNotNull(Lista_SRoles);
        }

        [TestMethod]
        public void VerificarVM()
        {
            string rolSeleccionado = "ROLE_USUARIOS_ADMINISTRADOR";
            UtilsString Utils = new UtilsString();
            UsSecRepository UsSec = new UsSecRepository();
            UsuariosSectores UsuarioSec = UsSec.BuscarUsuarioSector(2011);
            ViewModel vm = new ViewModel(UsuarioSec,rolSeleccionado);

            Assert.IsNotNull(vm);
        }

        [TestMethod]
        public void VerificarVMGuardarDatos()
        {
            string rolSeleccionado = "ROLE_USUARIOS_ADMINISTRADOR";
            List<ViewModel> lista = new List<ViewModel>();
            UtilsString Utils = new UtilsString();
            UsSecRepository UsSec = new UsSecRepository();
            UsuariosSectores UsuarioSec = UsSec.BuscarUsuarioSector(2011);
            ViewModel vm = new ViewModel(UsuarioSec, rolSeleccionado);
            List<UsuariosSectores> aux = UsSec.ListarTodosUsuariosSectores("39","juan",null);
            foreach (var item in aux)
            {
                ViewModel vModel = new ViewModel(item,rolSeleccionado);
                lista.Add(vModel);
            }
            Utils.ModificarDatosRolSegunChequeos(lista, rolSeleccionado);
            Assert.IsNull(lista);
        }

        [TestMethod]
        public void ConversionDeFechasCorrecta()
        {
            List<DateTime> listafechas=(new UtilsString()).conversionStringAFecha("22/08/1998-22/08/1998,23/08/1998-24/08/1998", "09:00-09:30,10:50-11:45");
            Assert.IsNotNull(listafechas);
        }

        [TestMethod]
        public void VerificarFechaCorrecta()
        {
            DateTime fecha = new DateTime(2018,03,22,12,4,50);
            bool resultado = (new UtilsString()).verificarFechaVigenciaDeRol(fecha);
            Assert.IsTrue(resultado);
        }
        [TestMethod]
        public void EnvioDeEmailCorrecto()
        {
            (new UtilsEmail()).EnviarEmail("guillekallap@hotmail.com", "guille", "ROLE_USUARIOS_ADMINISTRADOR");
        }
    }
}
