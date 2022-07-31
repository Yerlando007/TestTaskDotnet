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
        //для вывода сообщения на страницу
        {
            var result = await _userService.Login(phoneNumber, password);
            if (result == true)
            {
                ViewBag.Message = "Вы вошли";
            }
            else
            {
                ViewBag.Message = "Пароль или логин введен неверно";
            }
            return View();
        }
        //если работать напрямую с запросами
        // => Ok( await _userService.Login(phoneNumber, password) );


        [HttpGet]
        public async Task<IActionResult> GetUserRequests(string name)
           => Ok( await _userService.GetUserRequests(name) );


        //POST

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(string phoneNumber, string name, string password)
        {
            //для вывода сообщения на страницу
            var result = await _userService.RegisterNewUser(phoneNumber, name, password);
            if (result == true)
            {
                ViewBag.Message = "Регистрация прошла успешно";               
            }
            else
            {
                ViewBag.Message = "Ошибка при регистрации пользователя";
            }
            return View();
            //если работать напрямую с запросами
            //return result ? Ok(result) : BadRequest($"Ошибка при регистрации пользователя.");
        }
    }
}
