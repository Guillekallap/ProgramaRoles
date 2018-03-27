using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ProgramaRoles.Utils
{
    public class UtilsEmail
    {
        public void EnviarEmail(string email, string nombreUsuario, string roles){
            //La cadena "servidor" es el servidor de correo que enviará el mensaje
            string servidor = "smtp.gmail.com";
            // Crea el mensaje estableciendo quién lo manda y quién lo recibe
            MailMessage mensaje = new MailMessage(
               "guillekprueba1@gmail.com",
               email,
               "Se han Modificado sus roles en la cuenta de Usuario: "+nombreUsuario,
               "Le comento que se han modificado ciertos valores de sus roles de acuerdo a la solicitud que se ha enviado. Actualmente pose los roles de :"+roles);

            //Envía el mensaje.
            SmtpClient cliente = new SmtpClient(servidor);

            cliente.UseDefaultCredentials = false;
            cliente.Credentials = new System.Net.NetworkCredential("guillekprueba1@gmail.com", "12345678$g");
            cliente.Port = 587;
            cliente.Host = "smtp.gmail.com";
            cliente.EnableSsl = true;

            cliente.Send(mensaje);
        }
    }
}