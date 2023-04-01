using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Services;

namespace WordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnglishController : BaseController
    {
        private readonly IWordService<English> _englishService;

    
        public EnglishController(IWordService<English> englishService)
        {
            _englishService = englishService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _englishService.GetWordsWithRelations());      
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _englishService.GetWordWithRelations(id));
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
