using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WordsAPI.Controllers;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Services;

namespace WordsAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterDTO userRegisterDTO)
        {
            return CreateActionResult(await _userService.CreateUserAsync(userRegisterDTO));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return CreateActionResult(await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> AddWordToUserVocabulary(string word)
        {
            return CreateActionResult(await _userService.AddWordToUserVocabulary(HttpContext.User.Identity.Name,word));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserVocabulary()
        {
            return CreateActionResult(await _userService.GetUserVocabulary(HttpContext.User.Identity.Name));
        }
    }
}
