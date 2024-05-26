using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Service.DTOs
{
    public class SignInModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInWithPremiumUserCheckModel
    {
        public bool IsUserExists { get; set; }
        public bool IsSignInSuccessful { get; set; }
        public string Message { get; set; }
        public bool IsPremiumUser { get; set; }
    }

}
