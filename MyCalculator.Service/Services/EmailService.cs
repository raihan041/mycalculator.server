using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Service.Services
{
    public class EmailService : IEmailService
    {
        public IConfiguration Configuration { get; }
        public EmailService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void SendEmail(string toEmail, string subject, string HtmlMessage)
        {
            using (MailMessage mm = new MailMessage(Configuration["NetMail:sender"], toEmail))
            {

                mm.Subject = subject;
                string body = HtmlMessage;
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Configuration["NetMail:smtpHost"];
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(Configuration["NetMail:sender"], Configuration["NetMail:senderpassword"]);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt16(Configuration["NetMail:smtpPort"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(mm);
            }




        }
    }
}
