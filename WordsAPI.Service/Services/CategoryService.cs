using System;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Services;

namespace WordsAPI.Service.Services
{
	public class CategoryService : ICategoryService
	{
        private readonly IService<Category> _categoryService;

        public CategoryService(IService<Category> categoryService)
		{
			_categoryService = categoryService;
		}

		public CustomResponseDto<List<CategoryDTO>> GetAll()
		{
			var categories =  _categoryService.Where(z=>z.Id > 0)
				.Include(z=>z.Englishes);
			var categoryDtos = new List<CategoryDTO>();

			foreach (var item in categories)
			{
				categoryDtos.Add(new CategoryDTO() { Id = item.Id, Name = item.Name });
			}

			return CustomResponseDto<List<CategoryDTO>>.Success(200, categoryDtos);
		}
	}
}

