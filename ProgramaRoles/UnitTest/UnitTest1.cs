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

       
       
    }
}
