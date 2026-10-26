using System;
namespace CapaServicios.ServicesMail
{
    public class SystemSupportMail : MasterMailService
    {
        public SystemSupportMail()
        {
            senderMail = Environment.GetEnvironmentVariable("GYM_SMTP_USER");
            password = Environment.GetEnvironmentVariable("GYM_SMTP_PASSWORD");
            if (string.IsNullOrWhiteSpace(senderMail) || string.IsNullOrWhiteSpace(password))
                throw new InvalidOperationException("Configure GYM_SMTP_USER y GYM_SMTP_PASSWORD para enviar correo.");
            host = Environment.GetEnvironmentVariable("GYM_SMTP_HOST") ?? "smtp.gmail.com";
            port = 587; ssl = true;
            initializeSmtPCliente();
        }
    }
}
