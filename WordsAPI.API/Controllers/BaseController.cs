using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Dtos;
using SharedLibrary.Utililty;
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


            if (responseDto.Data is List<WordDTO> dataList && Request.QueryString.Value.IndexOf("rand=true") != -1)
            {

                var random = new Random();
                var randomizedData = dataList.OrderBy(x => random.Next()).ToList();

                responseDto.Data = (T)(object)randomizedData;
            }


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
