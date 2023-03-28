using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WordsAPI.Core.DTOs;

namespace WordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> responseDto)
        {
            if (responseDto.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = responseDto.StatusCode
                };
            }

            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.StatusCode
            };
        }
    }
}
