using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Service.DTOs
{
    public class UserModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class UserValidityModel
    {
        public bool IsUserEmailExists { get; set; }
        public bool IsPaymentStillValid { get; set; }
    }

    public class UserWithPaymentInfoModel
    {
        public UserModel UserInfo { get; set; }
        public List<PaymentModel> PaymentList { get; set; }
    }

    public class UserWithPaymentInputModel
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
