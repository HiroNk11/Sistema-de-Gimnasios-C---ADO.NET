using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicios.ServicesMail
{
    public abstract class MasterMailService
    {
        private SmtpClient smtpCliente;
        protected string senderMail { get; set; }
        protected string password { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }
        protected void initializeSmtPCliente()
        {
            if(!ssl) throw new InvalidOperationException("SMTP requiere STARTTLS.");
            smtpCliente = new SmtpClient { Timeout=15000, UseDefaultCredentials=false, DeliveryMethod=SmtpDeliveryMethod.Network };
            smtpCliente.Credentials = new NetworkCredential(senderMail, password);
            smtpCliente.Host = host;
            smtpCliente.Port = port;
            smtpCliente.EnableSsl = ssl;
        }

        public void sendEmail(string subject, string body, List<string> recipienteMail)
        {
            var mailMessage = new MailMessage();
            try
            {
                mailMessage.From = new MailAddress(senderMail);
                foreach (string mail in recipienteMail)
                {
                    mailMessage.To.Add(mail);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;
                smtpCliente.Send(mailMessage);


            }
            catch (Exception) { throw; }
            finally
            {
                mailMessage.Dispose();
                smtpCliente.Dispose();
            }
        }



    }
}
