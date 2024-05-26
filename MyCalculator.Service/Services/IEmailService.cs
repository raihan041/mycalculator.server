using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Service.Services
{
    public interface IEmailService
    {
        public void SendEmail(string email, string subject, string HtmlMessage);
    }
}
