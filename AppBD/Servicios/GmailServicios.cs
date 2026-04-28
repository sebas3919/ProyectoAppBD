using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.Servicios
{
    internal class GmailServicios
    {

        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string smtpUsername = "sebax39pj@gmail.com";
        private readonly string smtpPassword = "bhylpmsbkjckbaur";

        public async Task EnviarEmail(string destinatario, string asunto, string cuerpo)
        {
          
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add(destinatario);
                    mailMessage.Subject = asunto;
                    mailMessage.Body = cuerpo;
                    mailMessage.IsBodyHtml = true; // Si deseas enviar un correo con formato HTML
                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtpClient.EnableSsl = true; // Habilitar SSL para seguridad
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí (por ejemplo, registrar el error)
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }

    }
}
