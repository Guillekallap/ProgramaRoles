using System;
using System.Collections.Generic;
using ProgramaRoles.Utils;
using ProgramaRoles.Repository;
using ProgramaRoles.Models;

namespace ProgramaRoles.ViewModels
{
    public class ViewModelUsuarioRolHorario
    {
        public ViewModelUsuarioRolHorario() {

        }

        public int id { get; set; }
        public int idUsuarioSector { get; set; }
        public string nombreUsuario { get; set; }
        public string rolesTemporales { get; set; }
        public string email { get; set; }
        public DateTime fechaModificacion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public bool vigente { get; set; }
        public bool emailChked { get; set; }
        public bool Chked { get; set; }
        public string fechas { get; set; }
        public List<DateTime> listaFechas { get; set; }

        public ViewModelUsuarioRolHorario(ViewModelUsuarioRol vMuestra)
        {
            this.idUsuarioSector = vMuestra.id;
            this.nombreUsuario = vMuestra.nombreUsuario;
            this.Chked = false;
            this.emailChked = false;
            this.email = vMuestra.email;
            this.listaFechas = (new UtilsFecha()).listadoDeFechasPorUsuarioRolHorario((new UsSecRepository()).BuscarUsuarioSectorRolHorario(vMuestra.id));
        }

        public ViewModelUsuarioRolHorario(UsuarioRolHorario vmUSRH)
        {
            this.id = vmUSRH.id;
            this.idUsuarioSector = vmUSRH.idUsuarioSector;
            this.nombreUsuario = vmUSRH.nombreUsuario;
            this.rolesTemporales = vmUSRH.rolesTemporales;
            this.email = vmUSRH.email;
            this.fechaModificacion = vmUSRH.fechaModificacion;
            this.fechaInicio = vmUSRH.fechaInicio;
            this.fechaFin = vmUSRH.fechaFin;
            this.vigente = vmUSRH.vigente;
            this.emailChked = vmUSRH.emailChked;
            this.Chked = true;
        }

        public ViewModelUsuarioRolHorario(ViewModelUsuarioRolHorario vm)
        {
            this.idUsuarioSector = vm.idUsuarioSector;
            this.nombreUsuario = vm.nombreUsuario;
            this.rolesTemporales = vm.rolesTemporales;
            this.email = vm.email;
            this.fechaModificacion = vm.fechaModificacion;
            this.fechaInicio = vm.fechaInicio;
            this.fechaFin = vm.fechaFin;
            this.vigente = vm.vigente;
            this.emailChked = vm.emailChked;
            this.Chked = true;
        }

    }

    

}