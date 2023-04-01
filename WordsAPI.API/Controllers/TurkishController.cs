using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WordsAPI.Controllers;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Services;

namespace WordsAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurkishController : BaseController
    {
        private readonly IWordService<Turkish> _turkishService;


        public TurkishController(IWordService<Turkish> turkishService)
        {
            _turkishService = turkishService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _turkishService.GetWordsWithRelations());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _turkishService.GetWordWithRelations(id));
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
