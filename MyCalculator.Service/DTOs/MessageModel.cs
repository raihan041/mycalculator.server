using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Service.DTOs
{
    public class MessageModel
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
    }

    public class EmailModel
    {
        public string Subject { get; set; }
        public string HtmlMessage { get; set; }
        public string Receiver { get; set; }
    }
}
