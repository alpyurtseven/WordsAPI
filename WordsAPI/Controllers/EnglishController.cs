using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Services;

namespace WordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnglishController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<English> _service;


        public EnglishController(IService<English> service,IMapper mapper )
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var words = await _service.GetAllAsync();
            var wordDtos = _mapper.Map<List<WordDTO>>(words.ToList());

            return CreateActionResult(CustomResponseDto<List<WordDTO>>.Success(200, wordDtos));      
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var word = await _service.GetByIdAsync(id);
            var wordDto = _mapper.Map<WordDTO>(word);

            return CreateActionResult(CustomResponseDto<WordDTO>.Success(200, wordDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(English english)
        {
            var word = await _service.AddAsync(english);
            var wordDto = _mapper.Map<WordDTO>(word);

            return CreateActionResult(CustomResponseDto<WordDTO>.Success(201, wordDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(English english)
        {
            await _service.UpdateAsync(english);
  
            return CreateActionResult(CustomResponseDto<WordDTO>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var word = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(word);
      
            return CreateActionResult(CustomResponseDto<WordDTO>.Success(204));
        }
    }
}
