using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Service.DTOs
{
    public class PaymentModel
    {
        public string PaymentType { get; set; }
        public string PaymentMode { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardValidity { get; set; }
    }

    public class ConfirmPaymentModel
    {
        public string PaymentMode { get; set; }
        public string PaymentType { get; set; }
        public string CardNumberLastFourDigit { get; set; }
        public string CardValidity { get; set; }
        public string confirmationTokenId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
