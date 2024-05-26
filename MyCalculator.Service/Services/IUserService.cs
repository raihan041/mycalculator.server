using MyCalculator.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Service.Services
{
    public interface IUserService
    {
        public UserWithPaymentInfoModel GetUserInfoWithPaymentList(string userId);
        public List<UserWithPaymentInfoModel> GetAllUserInfoWithPaymentList();
        public SignInWithPremiumUserCheckModel SaveUserInfoWithPayment(UserWithPaymentInputModel model);
        public SignInWithPremiumUserCheckModel CheckUser(SignInModel model);
        public UserValidityModel CheckUserByEmail(string email);
    }
}
