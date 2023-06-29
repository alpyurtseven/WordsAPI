using System;
using SharedLibrary.Dtos;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.Services
{
	public interface ICategoryService
	{
        CustomResponseDto<List<CategoryDTO>> GetAll();
    }
}

