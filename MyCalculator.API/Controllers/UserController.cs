using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCalculator.Service.DTOs;
using MyCalculator.Service.Services;

namespace MyCalculator.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Route("api/User/getUserInfoWithPaymentList/{userId}")]
        [HttpGet]
        public async Task<ActionResult<UserWithPaymentInfoModel>> GetUserInfoWithPaymentList(string userId)
        {
            return _userService.GetUserInfoWithPaymentList(userId);
        }

        [Route("api/User/getAllUserInfoWithPaymentList")]
        [HttpGet]
        public async Task<ActionResult<List<UserWithPaymentInfoModel>>> GetAllUserInfoWithPaymentList()
        {
            return _userService.GetAllUserInfoWithPaymentList();
        }

    }
}
