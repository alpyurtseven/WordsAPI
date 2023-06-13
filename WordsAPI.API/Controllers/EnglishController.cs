using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Utililty;
using System.Linq;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Services;
using Utility = SharedLibrary.Utililty.Utility;

namespace WordsAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnglishController : BaseController
    {
        private readonly IWordService<English> _englishService;

    
        public EnglishController(IWordService<English> englishService)
        {
            _englishService = englishService;
        }

        [HttpGet]
        public async Task<IActionResult> All(ODataQueryOptions<English> queryOptions)
        {
            var words = await _englishService.GetWordsWithRelations(queryOptions);

            return CreateActionResult(words);      
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<IActionResult> GetById(int id, ODataQueryOptions<English> queryOptions)
        {
           
            return CreateActionResult(await _englishService.GetWordWithRelations(id,queryOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Save(WordDTO word)
        {
            return CreateActionResult(await _englishService.Save(word));
        }

        [HttpPut]
        public async Task<IActionResult> Update(WordDTO word)
        {
            return CreateActionResult(await _englishService.Save(word));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _englishService.Delete(id));
        }
    }
}
