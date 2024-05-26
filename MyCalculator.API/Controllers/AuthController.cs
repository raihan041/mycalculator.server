using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCalculator.Service.DTOs;
using MyCalculator.Service.Services;

namespace MyCalculator.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("api/auth/signIn")]
        [HttpPost]
        public async Task<ActionResult<SignInWithPremiumUserCheckModel>> SignInUser(SignInModel model)
        {
            return _userService.CheckUser(model);
        }

    }
}
