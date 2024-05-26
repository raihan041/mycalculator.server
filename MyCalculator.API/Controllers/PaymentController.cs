using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCalculator.Service.DTOs;
using MyCalculator.Service.Services;
using Stripe;

namespace MyCalculator.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public PaymentController( IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        [Route("api/payment/createConfirmIntent")]
        [HttpPost]
        public async Task<ActionResult<SignInWithPremiumUserCheckModel>> CreateConfirmIntent(UserWithPaymentInputModel model)
        {
            try
            {
                var userValidityCheck = _userService.CheckUserByEmail(model.Email);

                if (userValidityCheck.IsUserEmailExists)
                {
                    return new SignInWithPremiumUserCheckModel
                    {
                        IsPremiumUser = false,
                        IsSignInSuccessful = false,
                        IsUserExists = true,
                        Message = "User already exists with this email id"
                    };
                }
                

                var options = new PaymentIntentCreateOptions
                {
                    Amount = 1000,
                    Currency = "usd",
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                        AllowRedirects = "never"
                    },
                    Confirm = true,
                    ConfirmationToken = model.confirmationTokenId
                };
                var service = new PaymentIntentService();
                var paymentIntent = service.Create(options);



                var signInOutput = _userService.SaveUserInfoWithPayment(model);

                return signInOutput;
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        //[Route("api/payment/sendMail")]
        //[HttpPost]
        //public async Task<ActionResult<MessageModel>> SendMail()
        //{
        //    try
        //    {
        //        var messageModel = new MessageModel
        //        {
        //            IsSuccessful = false,
        //        };

        //        _emailService.SendEmail("test mail", "Test Stripe project", "Your test password: 1234");

        //        return messageModel;
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }

        //}
    }
}
