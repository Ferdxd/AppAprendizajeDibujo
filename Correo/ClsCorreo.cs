using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AppDibujoArtistico.Correo
{
    internal class ClsCorreo
    {
        private string EmailOrigen;
        private string Contraseña;

        public string emailOrigen()
        {
            EmailOrigen = "AppDibujoArtisticoAguaBlanca@gmail.com";
            return EmailOrigen;
        }
        public string contraseña()
        {
            Contraseña = "bdigwjygfgliuutx";
            return Contraseña;
        }
       
       // private string Contraseña = "ProyectoGraduacionUMG";
       
        public string enviarCorreo(string EmailDestino)
        {
            
            ClsCorreoVerificacion clsCorreoVerificacion = new ClsCorreoVerificacion();
            string tk = clsCorreoVerificacion.Token();
            MailMessage oMailMessage = new MailMessage(emailOrigen(),EmailDestino,"Codigo de Verificacion","Bienvenido a esta experiencia educativa \n" +
                "para poder confirmar tus datos tienes que ingresar el siguiente <p> codigo:<b>"+tk+ "</b> </p> en la ventana de confirmacion");

            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Host = "smtp.gmail.com";
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen(), contraseña()) ;
            oSmtpClient.Send(oMailMessage);
            oSmtpClient.Dispose();
            return tk;
        }

    }
}
