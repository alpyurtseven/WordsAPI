﻿using Microsoft.AspNetCore.OData.Query;
using SharedLibrary.Dtos;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.Services
{
    public interface IWordService<T> :IService<T> where T : AWord
    {
        Task<CustomResponseDto<List<WordDTO>>> GetWordsWithRelations(ODataQueryOptions<T>? queryOptions = null);
        Task<CustomResponseDto<WordDTO>> GetWordWithRelations(int id, ODataQueryOptions<T>? queryOptions = null);
        Task<Dictionary<string, IWord>> GetOrCreateTranslations(List<string> translations);
        Task<Dictionary<string, Category>> GetOrCreateItemCategory(List<string> categories);
        Task<CustomResponseDto<WordDTO>> Save(WordDTO word);
        Task<CustomResponseDto<WordDTO>> Delete(int id);
    }
}
