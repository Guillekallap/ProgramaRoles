using ProgramaRoles.Models;
using ProgramaRoles.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ProgramaRoles.ViewModels;

namespace ProgramaRoles.Utils
{
    public class UtilsUserController
    {
        public ViewModelUserValidez conversionViewModelUserValidez(List<UsuariosSectores> listUserAGrabar, List<UsuarioRolHorario> listaUSRHAGrabar, List<UsuarioRolHorario> listaUSRHAGrabarInvalido)
        {
            ViewModelUserValidez respuesta = new ViewModelUserValidez();

            if (listaUSRHAGrabarInvalido == null)
            {
                ViewModelUserValidez viewModelUserValido = new ViewModelUserValidez
                {
                    listaUsuario = new List<ViewModel>(),
                    listaUsuarioRolHorario = new List<ViewModelUsuarioRolHorario>()
                };

                foreach (UsuariosSectores user in listUserAGrabar)
                {
                    ViewModel vmUser = new ViewModel(user);
                    viewModelUserValido.listaUsuario.Add(vmUser);
                }
                foreach (UsuarioRolHorario USRH in listaUSRHAGrabar)
                {
                    ViewModelUsuarioRolHorario vmUserHora = new ViewModelUsuarioRolHorario(USRH);
                    viewModelUserValido.listaUsuarioRolHorario.Add(vmUserHora);
                }
                respuesta = viewModelUserValido;
            }
            else
            {
                ViewModelUserValidez viewModelUserInvalido = new ViewModelUserValidez
                {
                    listaUsuario = new List<ViewModel>(),
                    listaUsuarioRolHorario = new List<ViewModelUsuarioRolHorario>(),
                    listaUsuarioRolHorarioInvalido = new List<ViewModelUsuarioRolHorario>()
                };

                foreach (UsuariosSectores user in listUserAGrabar)
                {
                    ViewModel vmUser = new ViewModel(user);
                    viewModelUserInvalido.listaUsuario.Add(vmUser);
                }
                foreach (UsuarioRolHorario USRH in listaUSRHAGrabar)
                {
                    ViewModelUsuarioRolHorario vmUserHora = new ViewModelUsuarioRolHorario(USRH);
                    viewModelUserInvalido.listaUsuarioRolHorario.Add(vmUserHora);
                }
                foreach (UsuarioRolHorario USRHI in listaUSRHAGrabarInvalido)
                {
                    ViewModelUsuarioRolHorario vmUserHora = new ViewModelUsuarioRolHorario(USRHI);
                    viewModelUserInvalido.listaUsuarioRolHorarioInvalido.Add(vmUserHora);
                }

                respuesta = viewModelUserInvalido;
            }

            return respuesta;
        }
    }
}