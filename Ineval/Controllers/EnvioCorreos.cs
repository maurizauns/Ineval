using Ineval.DAL;
using Ineval.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace Ineval.Controllers
{
    public class EnvioCorreos
    {
        private static SwmContext db = new SwmContext();
        public static async Task<bool> SendAsync(string Id, string mensaje)
        {
            bool status = false;

            EmailParametros emailParametros = new EmailParametros();
            emailParametros = await db.EmailParametros.FirstOrDefaultAsync();

            Usuario usuario = new Usuario();
            usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.ApplicationUserId == Id);

            MailMessage msg = new MailMessage();

            if (usuario != null)
            {
                if (IsValidEmailAddress(usuario.Email))
                {
                    msg.To.Add(new MailAddress(usuario.Email.Trim()));
                }
            }

            string[] CorreosEnviar;

            CorreosEnviar = emailParametros.EmailCopia.Split(' ');

            foreach (string email_ in CorreosEnviar)
            {
                if (IsValidEmailAddress(email_.Trim()))
                {
                    msg.CC.Add(new MailAddress(email_.Trim()));
                }
            }

            
            msg.Subject = "Ineval";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //msg.Bcc.add

            string html = @"";
            html = html + @"<b>Nombre Usuario: </b> " + usuario.NombresCompletos + "<br/>";
            
            if (!String.IsNullOrEmpty(mensaje))
            {
                html = html + @"<b>Mensaje: </b> " + mensaje + "<br/>";
            }

            html = html + @"<p>Atentamente </p>";

            html = html + @"<p>Instituto Nacional de Evaluación Educativa </p>";

            html = html + @"<p>Este correo ha sido generado automáticamente, por favor no responda al mismo</p>";



            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;


            msg.From = new MailAddress(emailParametros.EmailPrincipal);
            SmtpClient cliete = new SmtpClient();

            cliete.Port = 587;
            cliete.EnableSsl = true;

            cliete.Host = "smtp.gmail.com";
            cliete.Credentials = new NetworkCredential(emailParametros.EmailPrincipal, EncryptDecrypt.Decrypt(emailParametros.EmailPassword));

            try
            {
                cliete.Send(msg);
                return status = true;
            }
            catch (Exception ex)
            {
                return status = false;

            }
        }

        private static bool IsValidEmailAddress(string emailAddress)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(emailAddress);
        }
    }
}