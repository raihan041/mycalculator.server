using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Data.Models
{
    public class Payment
    {
        public string Id { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentType { get; set; }
        public string CardType { get; set; }
        public string CardNumberLastFourDigit { get; set; }
        public string CardValidity { get; set; }
        public string UserId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; } = null;
    }
}
