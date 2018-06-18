using ProgramaRoles.Models;
using ProgramaRoles.Repository;
using ProgramaRoles.Utils;
using ProgramaRoles.ViewModels;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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

            UtilsRoles Utils = new UtilsRoles();
            UsSecRepository UsSec = new UsSecRepository();
            UsuariosSectores UsuarioSec = UsSec.BuscarUsuarioSector(4);
            List<Roles> Lista_Roles = UsSec.ListarTodosRoles();
            List<string> Lista_string = Utils.ConvertirDeListaDeRolesAListaNombreRoles(Lista_Roles);
            List<Sroles> Lista_SRoles = Utils.ParsearPropiedadRoles(UsuarioSec, Lista_string);
            //string StringResultante = Utils.TraducirRolesAString(Lista_SRoles);
            Assert.IsNotNull(Lista_SRoles);
        }

        [TestMethod]
        public void ParsearPropiedadRolesNulo()
        {

            UtilsRoles Utils = new UtilsRoles();
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
        public void verificarListaDeFechas()
        {
            List<DateTime> listafechas = (new UtilsFecha()).conversionStringAFecha("22/08/2018,23/08/2018,24/08/2018,26/08/2018,28/08/2018");
            List<DateTime> listaFinalFechas = (new UtilsFecha()).identificarFechaInicioFechaFin(listafechas);
            Assert.IsNotNull(listaFinalFechas);
        }

        [TestMethod]
        public void verificarListaDeFechasPares()
        {
            List<DateTime> listafechas2 = (new UtilsFecha()).conversionStringAFecha("20/08/2018,22/08/2018,24/08/2018,26/08/2018,28/08/2018");
            List<DateTime> listaFinalFechas2 = (new UtilsFecha()).identificarFechaInicioFechaFin(listafechas2);
            Assert.IsNull(listaFinalFechas2);
        }

        [TestMethod]
        public void verificarListaDeFechasConjuntasFinal()
        {
            List<DateTime> listafechas3 = (new UtilsFecha()).conversionStringAFecha("20/08/2018,22/08/2018,24/08/2018,25/08/2018,26/08/2018,28/08/2018");
            List<DateTime> listaFinalFechas3 = (new UtilsFecha()).identificarFechaInicioFechaFin(listafechas3);
            Assert.IsNull(listaFinalFechas3);
        }

        [TestMethod]
        public void verificarListaDeFechasConjuntas()
        {
            List<DateTime> listafechas4 = (new UtilsFecha()).conversionStringAFecha("20/08/2018,21/08/2018,22/08/2018,24/08/2018,25/08/2018,26/08/2018,27/08/2018");
            List<DateTime> listaFinalFechas4 = (new UtilsFecha()).identificarFechaInicioFechaFin(listafechas4);
            Assert.IsNull(listaFinalFechas4);
        }

        [TestMethod]
        public void verificarListaDeFechasRandom()
        {
            List<DateTime> listafechas4 = (new UtilsFecha()).conversionStringAFecha("20/08/2018,22/08/2018,24/08/2018,25/08/2018,27/08/2018,28/08/2018,30/08/2018");
            List<DateTime> listaFinalFechas4 = (new UtilsFecha()).identificarFechaInicioFechaFin(listafechas4);
            Assert.IsNull(listaFinalFechas4);
        }

        [TestMethod]
        public void verificarFechaUnDía()
        {
            List<DateTime> listafechas4 = (new UtilsFecha()).conversionStringAFecha("31/05/2018");
            List<DateTime> listaFinalFechas4 = (new UtilsFecha()).identificarFechaInicioFechaFin(listafechas4);
            Assert.IsNotNull(listaFinalFechas4);
        }

        [TestMethod]
        public void verificarFechaActual()
        {
            List<DateTime> listafechas4 = (new UtilsFecha()).conversionStringAFecha("30/05/2018");
            List<DateTime> listaFinalFechas4 = (new UtilsFecha()).identificarFechaInicioFechaFin(listafechas4);
            Assert.IsNotNull(listaFinalFechas4);
        }

        [TestMethod]
        public void verificarFechaActualMásSiguiente()
        {
            List<DateTime> listafechas4 = (new UtilsFecha()).conversionStringAFecha("30/05/2018,31/05/2018");
            List<DateTime> listaFinalFechas4 = (new UtilsFecha()).identificarFechaInicioFechaFin(listafechas4);
            Assert.IsNotNull(listaFinalFechas4);
        }

        [TestMethod]
        public void verificarFechaActualMásSiguienteYAtrasados()
        {
            List<DateTime> listafechas4 = (new UtilsFecha()).conversionStringAFecha("29/05/2018,30/05/2018,31/05/2018");
            List<DateTime> listaFinalFechas4 = (new UtilsFecha()).identificarFechaInicioFechaFin(listafechas4);
            Assert.IsNotNull(listaFinalFechas4);
        }

        [TestMethod]
        public void ordenarRolesTemporalesListaRolesMenorARoles()
        {
            UsuariosSectores user = (new UsSecRepository()).BuscarUsuarioSector(1007);
            List<string> listaRoles = new List<string>();
            listaRoles.Count();
            listaRoles.Add("ROLE_USUARIOS_ADMINISTRADOR");
            listaRoles.Add("ROLE_INGRESO");
            string Roles = (new UtilsString()).OrdenarListaDeRolesTemporales(user.roles,listaRoles);
            Assert.IsNotNull(Roles);
        }

        [TestMethod]
        public void ordenarRolesTemporalesListaRolesMayorARoles()
        {
            UsuariosSectores user = (new UsSecRepository()).BuscarUsuarioSector(1006);
            List<string> listaRoles = new List<string>();
            //ROLE_USUARIOS_ADMINISTRADOR,ROLE_ADMISION_BASE,ROLE_TURNOS_MULTISECTOR,ROLE_TURNOS,ROLE_REGISTRO_MEDICO_CONSULTA,ROLE_REGISTRO_MEDICO_EDICION,ROLE_INGRESO,ROLE_VACUNAS_EDICION
            string Roles = (new UtilsString()).OrdenarListaDeRolesTemporales(user.roles, listaRoles);
            Assert.IsNotNull(Roles);
        }

        [TestMethod]
        public void ordenarRolesTemporalesListaRolesNuloARoles()
        {
            UsuariosSectores user = (new UsSecRepository()).BuscarUsuarioSector(1007);
            List<string> listaRoles = new List<string>();
            listaRoles.Add("");
            string Roles = (new UtilsString()).OrdenarListaDeRolesTemporales(user.roles, listaRoles);
            Assert.IsNotNull(Roles);
        }

        [TestMethod]
        public void ordenarRolesTemporalesListaRolesNuloARolesNulo()
        {
            UsuariosSectores user = (new UsSecRepository()).BuscarUsuarioSector(1007);
            List<string> listaRoles = new List<string>();
            listaRoles.Count();
            listaRoles.Add("ROLE_USUARIOS_ADMINISTRADOR");
            listaRoles.Add("ROLE_INGRESO");
            string Roles = (new UtilsString()).OrdenarListaDeRolesTemporales(user.roles, listaRoles);
            Assert.IsNotNull(Roles);
        }
        [TestMethod]
        public void ordenarRolesTemporalesUsuarioSector3()
        {
            UsuariosSectores user = (new UsSecRepository()).BuscarUsuarioSector(3);
            List<string> listaRoles = new List<string>();
            listaRoles.Count();
            listaRoles.Add("ROLE_USUARIOS_ADMINISTRADOR");
            listaRoles.Add("ROLE_INGRESO");
            string Roles = (new UtilsString()).OrdenarListaDeRolesTemporales(user.roles, listaRoles);
            Assert.IsNotNull(Roles);
        }

        [TestMethod]
        public void ComprobarListadoDeFechasUSRH()
        {
            List<UsuarioRolHorario> listadoFechas = (new UsSecRepository()).ListarUsuarioRolHorario(1006, (new UtilsFecha()).conversionStringAFecha("14/06/2018").First(), (new UtilsFecha()).conversionStringAFecha("30/07/2018").First());
            //List<DateTime> listadoFec = (new UtilsString()).listadoDeFechasPorUsuarioRolHorario((new UsSecRepository()).BuscarUsuarioSectorRolHorario(1007));        
            Assert.IsNotNull(listadoFechas);
        }

        [TestMethod]
        public void eliminarFechasMenorALaActual()
        {
            List<DateTime> listaFechas = (new UtilsFecha()).conversionStringAFecha("20/08/2018,22/08/2018,24/08/2018,25/08/2018,27/08/2018,28/08/2018,30/08/2018,18/05/2018");
            listaFechas=(new UtilsFecha()).identificarFechaInicioFechaFin(listaFechas);
            Assert.IsNotNull(listaFechas);
        }

        [TestMethod]
        public void acotarFechasPrueba1InicioMenorAFechaInicioYFinMayorAFechaInicio()
        {
            List<UsuarioRolHorario> listadoFechas = (new UsSecRepository()).ListarUsuarioRolHorario(1006, (new UtilsFecha()).conversionStringAFecha("18/06/2018").First(), (new UtilsFecha()).conversionStringAFecha("10/08/2018").First());
            (new UtilsFecha()).AcortarFechas(listadoFechas, (new UtilsFecha()).conversionStringAFecha("18/06/2018").First(), (new UtilsFecha()).conversionStringAFecha("10/08/2018").First());
            Assert.IsNotNull(listadoFechas);
        }


        [TestMethod]
        public void constructorViewModelUsuarioRolHorario()
        {

            List<UsuariosSectores> listUser = (new UsSecRepository()).ListarTodosUsuariosSectores(null,null,null);

            List<ViewModelUsuarioRolHorario> listUSRH = new List<ViewModelUsuarioRolHorario>();
            foreach (var User in listUser)
            {
                ViewModelUsuarioRol vmm = new ViewModelUsuarioRol(User, (new UsSecRepository()).ListarTodosRoles());
                ViewModelUsuarioRolHorario vm = new ViewModelUsuarioRolHorario(vmm);
                listUSRH.Add(vm);
            }
            Assert.IsNotNull(listUser);
        }

        //[TestMethod]
        //public void ConversionDeFechasCorrecta()
        //{
        //    List<DateTime> listafechas = (new UtilsString()).conversionStringAFecha("22/08/2018-22/08/2018,23/08/2018-24/08/2018", "09:00-09:30,10:50-11:45");
        //    Assert.IsNotNull(listafechas);
        //}

        //[TestMethod]
        //public void VerificarFechaCorrecta()
        //{
        //    DateTime fecha = new DateTime(2018,03,22,12,4,50);
        //    bool resultado = (new UtilsString()).verificarFechaVigenciaDeRol(fecha);
        //    Assert.IsTrue(resultado);
        //}
        //[TestMethod]
        //public void EnvioDeEmailCorrecto()
        //{
        //    (new UtilsFecha()).EnviarCorreo("soportetecnicosalud@gmail.com", "soporte", "ROLE_USUARIOS_ADMINISTRADOR,ROLE_ADMINISION_BASE",true);            
        //}
    }
}
