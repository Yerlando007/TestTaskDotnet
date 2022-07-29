using Microsoft.AspNetCore.Mvc;
using TestTaskDotnet.Interfaces;

namespace TestTaskDotnet.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        //GET

        [HttpGet]
        public async Task<IActionResult> Login(string phoneNumber, string password)
            => Ok( await _userService.Login(phoneNumber, password) );

        [HttpGet]
        public async Task<IActionResult> GetUserRequests(string userName)
           => Ok( await _userService.GetUserRequests(userName) );


        //POST

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(string phoneNumber, string name, string password)
        {
            var result = await _userService.RegisterNewUser(phoneNumber, name, password);
            return result ? Ok(result) : BadRequest($"Ошибка при регистрации пользователя.");
        }
    }
}
