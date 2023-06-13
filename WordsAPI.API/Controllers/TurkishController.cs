using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using WordsAPI.Controllers;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Services;

namespace WordsAPI.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TurkishController : BaseController
    {
        private readonly IWordService<Turkish> _turkishService;


        public TurkishController(IWordService<Turkish> turkishService)
        {
            _turkishService = turkishService;
        }

        [HttpGet]
        public async Task<IActionResult> All(ODataQueryOptions<Turkish> queryOptions)
        {
            return CreateActionResult(await _turkishService.GetWordsWithRelations(queryOptions));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, ODataQueryOptions<Turkish> queryOptions)
        {
            return CreateActionResult(await _turkishService.GetWordWithRelations(id,queryOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Save(WordDTO word)
        {
            return CreateActionResult(await _turkishService.Save(word));
        }

        [HttpPut]
        public async Task<IActionResult> Update(WordDTO word)
        {
            return CreateActionResult(await _turkishService.Save(word));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _turkishService.Delete(id));
        }
    }
}
