using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WordsAPI.Controllers;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Services;

namespace WordsAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(UserLoginDTO loginDto)
        {
            return CreateActionResult(await _authenticationService.CreateTokenAsync(loginDto));
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDTO loginDto)
        {
            return CreateActionResult(_authenticationService.CreateTokenByClient(loginDto));
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokentDTO token)
        {
            return CreateActionResult(await _authenticationService.RevokeRefreshToken(token.Token));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokentDTO token)
        {
            return CreateActionResult(await _authenticationService.CreateTokenByRefreshToken(token.Token));
        }
    }
}
