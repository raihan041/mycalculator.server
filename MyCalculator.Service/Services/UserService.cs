using MyCalculator.Common.Helpers;
using MyCalculator.Data.Models;
using MyCalculator.Data.Repositories;
using MyCalculator.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IPaymentRepository paymentRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
            _emailService = emailService;
        }


        public UserWithPaymentInfoModel GetUserInfoWithPaymentList(string userId)
        {
            try
            {
                UserWithPaymentInfoModel userModel = new UserWithPaymentInfoModel();
                var user = _userRepository.GetById(userId);
                if (user != null)
                {

                    userModel = new UserWithPaymentInfoModel
                    {
                        UserInfo = new UserModel
                        {
                            Email = user.Email,
                            FullName = user.FullName,
                        },
                        PaymentList = _paymentRepository.GetAll()
                                .Where(x => x.UserId == userId)
                                .Select(x => new PaymentModel
                                {
                                    CardNumber = x.CardNumberLastFourDigit,
                                    CardType = x.CardType,
                                    CardValidity = x.CardValidity,
                                    PaymentType = x.PaymentType,
                                    PaymentMode = x.PaymentMode,
                                }).ToList()
                    };


                }

                return userModel;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<UserWithPaymentInfoModel> GetAllUserInfoWithPaymentList()
        {
            try
            {
                var outputList = new List<UserWithPaymentInfoModel>();
                
                foreach (var user in _userRepository.GetAll())
                {
                    var userModel = new UserWithPaymentInfoModel
                    {
                        UserInfo = new UserModel
                        {
                            Email = user.Email,
                            FullName = user.FullName,
                        },
                        PaymentList = _paymentRepository.GetAll()
                                .Where(x => x.UserId == user.Id)
                                .Select(x => new PaymentModel
                                {
                                    CardNumber = x.CardNumberLastFourDigit,
                                    CardType = x.CardType,
                                    CardValidity = x.CardValidity,
                                    PaymentType = x.PaymentType,
                                    PaymentMode= x.PaymentMode,
                                }).ToList()
                    };

                    outputList.Add(userModel);
                }
                          
                return outputList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public SignInWithPremiumUserCheckModel SaveUserInfoWithPayment(UserWithPaymentInputModel model)
        {
            SignInWithPremiumUserCheckModel signInWithPremiumUserCheckModel = new SignInWithPremiumUserCheckModel
            {
                IsSignInSuccessful = true,
            };
            try
            {
                var password = PasswordHelper.GenerateRandomPassword(8);
                
                var user = _userRepository.Create(new User
                {
                    FullName = model.Name,
                    Email = model.Email,
                    Username = model.Email,
                    Password = CryptographyHelper.ComputeSha256Hash(password),
                    
                });

                var payment = _paymentRepository.Create(new Payment
                {
                    CardNumberLastFourDigit = model.CardNumberLastFourDigit,
                    CardValidity = model.CardValidity,
                    PaymentType = model.PaymentType,
                    PaymentMode = model.PaymentMode,
                    UserId = user.Id
                });

                signInWithPremiumUserCheckModel.Message = "Successfully created.";
                signInWithPremiumUserCheckModel.IsPremiumUser = true;




                _emailService.SendEmail(model.Email, "My Calculator Account", "Dear <strong>" + model.Name + ",</strong><p>Your account has been created successfully.</p><p>Your password is: <strong>" + password+ "</strong></p>");
            }
            catch (Exception e)
            {
                signInWithPremiumUserCheckModel.Message = e.Message;
                signInWithPremiumUserCheckModel.IsPremiumUser = false;
                signInWithPremiumUserCheckModel.IsSignInSuccessful = false;
            }

            return signInWithPremiumUserCheckModel;
        }

        public SignInWithPremiumUserCheckModel CheckUser(SignInModel model)
        {
            try
            {
                SignInWithPremiumUserCheckModel outputModel = new SignInWithPremiumUserCheckModel
                {
                    IsPremiumUser = false
                };
                var user = _userRepository.GetByEmail(model.Email);
                if(user == null)
                {
                    outputModel.IsSignInSuccessful = false;
                    outputModel.Message = "No User found!";
                    return outputModel;
                }
                else if(user.Password != CryptographyHelper.ComputeSha256Hash(model.Password))
                {
                    outputModel.IsSignInSuccessful = false;
                    outputModel.Message = "Password not matched!";
                    return outputModel;
                }


                var paymentList = _paymentRepository.GetUserPaymentList(user.Id);

                if(paymentList == null || paymentList.Count == 0)
                {
                    outputModel.IsSignInSuccessful = true;
                    outputModel.Message = "No Premium User found!";
                    outputModel.IsPremiumUser = false;
                }
                else
                {
                    outputModel.IsSignInSuccessful = true;
                    outputModel.Message = "Premium User found!";
                    outputModel.IsPremiumUser = true;
                }


                return outputModel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserValidityModel CheckUserByEmail(string email)
        {
            UserValidityModel userValidityModel = new UserValidityModel
            {
                IsUserEmailExists = false,
                IsPaymentStillValid = false,
            };

            var user = _userRepository.GetByEmail(email);
            if (user != null)
            {
                userValidityModel.IsUserEmailExists = true;
                var paymentList = _paymentRepository.GetUserPaymentList(user.Id);

                if (paymentList == null || paymentList.Count == 0)
                {
                    userValidityModel.IsPaymentStillValid = false;
                }
                else
                {
                    userValidityModel.IsPaymentStillValid = true;
                }
            }

            return userValidityModel;
        }
    }
}
