﻿using Microsoft.AspNetCore.Mvc;
using TestTaskDotnet.Interfaces;
using TestTaskDotnet.Models.RequestModels;

namespace TestTaskDotnet.Controllers
{
    public class RequestController : Controller
    {
        private readonly ILogger<RequestController> _logger;
        private readonly IRequestService _requestService;

        public RequestController(ILogger<RequestController> logger, IRequestService requestService)
        {
            _logger = logger;
            _requestService = requestService;
        }

        public IActionResult MessagePage()
        {
            return View();
        }

        //GET

        [HttpGet]
        public IActionResult GetAllRequests()
            => Ok(_requestService.GetAllRequests());

        [HttpGet]
        public async Task<IActionResult> GetRequest(int requestID)
            => Ok(await _requestService.GetRequest(requestID));

        [HttpGet]
        public IActionResult GetRequestsHistory()
            => Ok(_requestService.GetRequestsHistory());


        //POST

        [HttpPost]
        public async Task<IActionResult> AddRequestToUser(int requestID, string userName)
        {
            var result = await _requestService.AddRequestToUser(requestID, userName);
            return result ? Ok(result) : BadRequest($"Ошибка при добавлении заявки пользователю {userName}.");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRequestFromUser(int requestID, string userName)
        {
            var result = await _requestService.RemoveRequestFromUser(requestID, userName);
            return result ? Ok(result) : BadRequest($"Ошибка при удалении заявки у пользователя {userName}.");
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(string phoneNumber, string fio, string email, RequestType type)
        {
            var result = await _requestService.AddRequest(phoneNumber, fio, email, type);
            return result ? Ok(result) : BadRequest("Ошибка при создании заявки.");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRequest(int Id)
        {
            var result = await _requestService.RemoveRequest(Id);
            return result ? Ok(result) : BadRequest("Ошибка при удалении заявки.");
        }

        [HttpPost]
        public async Task<IActionResult> CancelRequest(int Id)
        {
            var result = await _requestService.CancelRequest(Id);
            return result ? Ok(result) : BadRequest($"Заявка с ID {Id} не существует.");;
        }

        [HttpPost]
        public async Task<IActionResult> CloseRequest(int Id)
        {
            var result = await _requestService.CloseRequest(Id);
            return result ? Ok(result) : BadRequest($"Заявка с ID {Id} не существует."); ;
        }
    }
}
