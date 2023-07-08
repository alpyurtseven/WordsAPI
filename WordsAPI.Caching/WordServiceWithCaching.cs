using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SharedLibrary.Dtos;
using SharedLibrary.Utililty;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Repositories;
using WordsAPI.Core.Services;
using WordsAPI.Core.UnitOfWorks;
using WordsAPI.Repository.Repositories;
using WordsAPI.Repository.UnitOfWorks;

namespace WordsAPI.Caching
{
	public class WordServiceWithCaching<T>: IWordService<T> where T : AWord
	{
        private const string CacheWordKey = "wordsCache";
        private readonly IMemoryCache _cache;
        private readonly IWordRepository<T> _repository;
        private readonly IService<Turkish> _turkishService;
        private readonly IService<Category> _categoryService;
        private readonly IService<English> _englishService;
        private readonly IUnitOfWork _unitOfWork;

        public WordServiceWithCaching(IMemoryCache cache, IWordRepository<T> repository, IService<Turkish> turkishService,IService<English> englishService, IService<Category> categoryService, IUnitOfWork unitOfWork)
		{
            _cache = cache;
            _repository = repository;
            _turkishService = turkishService;
            _categoryService = categoryService;
            _englishService = englishService;
            _unitOfWork = unitOfWork;

            if (!_cache.TryGetValue(CacheWordKey, out _))
            { 
               CacheAllWordsAsync();
            }
		}

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllWordsAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllWordsAsync();

            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<WordDTO>> Delete(int id)
        {
            if (typeof(T) == typeof(English))
            {
                var word = await _englishService.GetByIdAsync(id);

                word.Status = 0;

                await _englishService.UpdateAsync(word);

                await CacheAllWordsAsync();

                return CustomResponseDto<WordDTO>.Success(201, new WordDTO() { Word = word.NormalizedWord ?? "" });
            }
            else
            {
                var word = await _turkishService.GetByIdAsync(id);

                word.Status = 0;

                await _turkishService.UpdateAsync(word);

                await CacheAllWordsAsync();

                return CustomResponseDto<WordDTO>.Success(201, new WordDTO() { Word = word.NormalizedWord ?? "" });
            }
        }

        public async Task<IQueryable<T>> GetAllAsync(ODataQueryOptions<User> queryOptions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(int id, ODataQueryOptions<User> queryOptions = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<string, Category>> GetOrCreateItemCategory(List<string> categories)
        {
            var allCategories = await _categoryService.GetAllAsync();
            var existingCategories = allCategories.Where(c => categories.Select(nc => nc).Contains(c.Name)).ToDictionary(c => c.Name ?? "", c => c);

            foreach (var category in categories)
            {
                string? matchedCategory = null;

                foreach (var item in allCategories.ToList())
                {
                    if (Utility.NormalizeWord(item.Name) == Utility.NormalizeWord(category))
                    {
                        matchedCategory = item.Name;
                    }
                }

                if (matchedCategory == null)
                {
                    Category newCategory = new Category() { Name = category, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
                    await _categoryService.AddAsync(newCategory);
                    existingCategories.Add(newCategory.Name, newCategory);
                }
            }

            return existingCategories;
        }

        public async Task<Dictionary<string, IWord>> GetOrCreateTranslations(List<string> translations)
        {
            var allTranslations = await _turkishService.GetAllAsync();
            var existingTranslations = allTranslations.Where(c => translations.Select(nc => Utility.NormalizeWord(nc)).Contains(c.NormalizedWord)).ToDictionary(c => c.NormalizedWord ?? "", c => (IWord)c);

            foreach (var translation in translations)
            {
                var matchedTranslation = allTranslations.SingleOrDefault(z => z.NormalizedWord == Utility.NormalizeWord(translation));

                if (matchedTranslation == null)
                {
                    if (typeof(T) == typeof(English))
                    {
                        Turkish newTurkish = new Turkish() { Word = translation, NormalizedWord = Utility.NormalizeWord(translation), Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Categories = new List<Category>() };

                        existingTranslations.Add(newTurkish.NormalizedWord, await _turkishService.AddAsync(newTurkish));
                    }
                    else
                    {
                        English newEnglish = new English() { Word = translation, NormalizedWord = Utility.NormalizeWord(translation), Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Categories = new List<Category>() };

                        existingTranslations.Add(newEnglish.NormalizedWord, await _englishService.AddAsync(newEnglish));
                    }
                }
                else
                {
                    var tempTranslation = existingTranslations[Utility.NormalizeWord(translation)];

                    var dbWord = _turkishService.Where(z => z.NormalizedWord == translation).Include(z => z.Categories).ToList();
                    existingTranslations.Remove(Utility.NormalizeWord(translation));
                    existingTranslations.Add(tempTranslation.NormalizedWord ?? "", tempTranslation);
                }
            }

            return existingTranslations;
        }

        public Task<CustomResponseDto<List<WordDTO>>> GetWordsWithRelations(ODataQueryOptions<T>? queryOptions = null)
        {
            List<WordDTO> wordsDto = new List<WordDTO>();
            var words = _cache.Get<List<T>>(CacheWordKey);

            foreach (var item in words)
            {
                wordsDto.Add(new WordDTO()
                {
                    Id = item.Id,
                    Word = item.Word ?? "",
                    Translations = item.getTranslations(),
                    Categories = item.getCategories()
                });
            }

            return Task.FromResult(CustomResponseDto<List<WordDTO>>.Success(200, wordsDto));
        }

        public async Task<CustomResponseDto<WordDTO>> GetWordWithRelations(int id, ODataQueryOptions<T>? queryOptions = null)
        {
            var word = await _repository.GetWordWithRelations(id, queryOptions);

            return CustomResponseDto<WordDTO>.Success(200, new WordDTO() { Word = word.Word ?? "", Translations = word.getTranslations(), Categories = word.getCategories() });
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<WordDTO>> Save(WordDTO word)
        {
            var categories = await GetOrCreateItemCategory(word.Categories);
            var translations = await GetOrCreateTranslations(word.Translations);

            if (typeof(T) == typeof(English))
            {
                var allEnglishWords = await _englishService.GetAllAsync();
                var matchedEnglishEntity = allEnglishWords.Where(w => w.NormalizedWord == Utility.NormalizeWord(word.Word)).FirstOrDefault();
                var alreadyExistsEnglishWord = GetExistEntity<English>(allEnglishWords, word.Word);
                var newEnglish = new English() { Word = word.Word, NormalizedWord = Utility.NormalizeWord(word.Word), Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Categories = new List<Category>(), Translations = new List<Turkish>() };


                if (matchedEnglishEntity != null)
                {
                    newEnglish = matchedEnglishEntity;
                    newEnglish.Categories = new List<Category>();
                    newEnglish.Translations = new List<Turkish>();
                }


                foreach (var category in word.Categories)
                {
                    if (categories.TryGetValue(category, out var existingCategory))
                    {
                        if (alreadyExistsEnglishWord != null && alreadyExistsEnglishWord.Categories != null)
                        {
                            var existCategory = alreadyExistsEnglishWord.Categories.FirstOrDefault(c => c.Id == existingCategory.Id);
                            if (existCategory == null)
                            {
                                alreadyExistsEnglishWord.Categories.Add(existingCategory);
                            }
                        }
                        else
                        {
                            newEnglish.Categories.Add(existingCategory);
                        }
                    }
                }

                foreach (var translation in word.Translations)
                {
                    if (translations.TryGetValue(Utility.NormalizeWord(translation), out var existingtranslation))
                    {
                        if (alreadyExistsEnglishWord != null && alreadyExistsEnglishWord.Translations != null)
                        {
                            if (existingtranslation is Turkish turkishWord && existingtranslation.Categories != null)
                            {
                                var existTranslation = alreadyExistsEnglishWord.Translations.SingleOrDefault(c => c.Id == existingtranslation.Id);
                                if (existTranslation == null)
                                {
                                    foreach (var category in word.Categories)
                                    {
                                        if (categories.TryGetValue(category, out var existingCategory) && alreadyExistsEnglishWord.Categories != null)
                                        {
                                            var existCategory = alreadyExistsEnglishWord.Categories.SingleOrDefault(c => c.Id == existingCategory.Id);
                                            if (existCategory == null)
                                            {
                                                existingtranslation.Categories.Add(existingCategory);
                                            }
                                        }
                                    }

                                    newEnglish.Translations.Add((Turkish)existingtranslation);
                                }
                            }
                        }
                        else
                        {
                            newEnglish.Translations.Add((Turkish)existingtranslation);
                        }
                    }
                }

                newEnglish.Status = word.Status;

                await _categoryService.UpdateRangeAsync(newEnglish.Categories.ToList());
                await _turkishService.UpdateRangeAsync(newEnglish.Translations.ToList());

                if (alreadyExistsEnglishWord != null)
                {
                    await _englishService.UpdateAsync(newEnglish);
                }
                else
                {
                    await _englishService.AddAsync(newEnglish);
                }
            }
            else
            {
                var allTurkishWords = await _turkishService.GetAllAsync();
                var matchedTurkishEntity = allTurkishWords.Where(w => w.NormalizedWord == Utility.NormalizeWord(word.Word)).FirstOrDefault();
                var alreadyExistsTurkishWord = GetExistEntity<Turkish>(allTurkishWords, word.Word);
                var newTurkish = new Turkish() { Word = word.Word, NormalizedWord = Utility.NormalizeWord(word.Word), Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Categories = new List<Category>(), Translations = new List<English>() };


                if (matchedTurkishEntity != null)
                {
                    newTurkish = matchedTurkishEntity;
                    newTurkish.Categories = new List<Category>();
                    newTurkish.Translations = new List<English>();
                }


                foreach (var category in word.Categories)
                {
                    if (categories.TryGetValue(category, out var existingCategory))
                    {
                        if (alreadyExistsTurkishWord != null && alreadyExistsTurkishWord.Categories != null)
                        {
                            var existCategory = alreadyExistsTurkishWord.Categories.FirstOrDefault(c => c.Id == existingCategory.Id);
                            if (existCategory == null)
                            {
                                alreadyExistsTurkishWord.Categories.Add(existingCategory);
                            }
                        }
                        else
                        {
                            newTurkish.Categories.Add(existingCategory);
                        }
                    }
                }

                foreach (var translation in word.Translations)
                {
                    if (translations.TryGetValue(Utility.NormalizeWord(translation), out var existingtranslation) && existingtranslation.Categories != null)
                    {
                        if (alreadyExistsTurkishWord != null && alreadyExistsTurkishWord.Translations != null)
                        {
                            if (existingtranslation is Turkish turkishWord)
                            {
                                var existTranslation = alreadyExistsTurkishWord.Translations.FirstOrDefault(c => c.Id == existingtranslation.Id);
                                if (existTranslation == null)
                                {
                                    foreach (var category in word.Categories)
                                    {
                                        if (categories.TryGetValue(category, out var existingCategory) && alreadyExistsTurkishWord.Categories != null)
                                        {
                                            var existCategory = alreadyExistsTurkishWord.Categories.FirstOrDefault(c => c.Id == existingCategory.Id);
                                            if (existCategory == null)
                                            {
                                                existingtranslation.Categories.Add(existingCategory);
                                            }
                                        }
                                    }

                                    newTurkish.Translations.Add((English)existingtranslation);
                                }
                            }
                        }
                        else
                        {
                            foreach (var category in word.Categories)
                            {
                                if (categories.TryGetValue(category, out var existingCategory))
                                {
                                    existingtranslation.Categories.Add(existingCategory);
                                }
                            }

                            newTurkish.Translations.Add((English)existingtranslation);
                        }
                    }
                }

                newTurkish.Status = word.Status;

                await _categoryService.UpdateRangeAsync(newTurkish.Categories.ToList());
                await _englishService.UpdateRangeAsync(newTurkish.Translations.ToList());

                if (alreadyExistsTurkishWord != null)
                {
                    await _turkishService.UpdateAsync(newTurkish);
                }
                else
                {
                    await _turkishService.AddAsync(newTurkish);
                }
            }

            await CacheAllWordsAsync();

            return CustomResponseDto<WordDTO>.Success(201, word);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(List<T> entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }


        public async Task CacheAllWordsAsync()
        {
            var words = _repository.GetWordsWithRelations();
          

            _cache.Set(CacheWordKey,await words);
        }

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
        public T? GetExistEntity<T>(IQueryable<IWord> words, string searchedWord) where T : class, IWord
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
        {
            if (typeof(T) == typeof(English))
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                English alreadyExistsWord = words.OfType<English>().Include(c => c.Categories).Include(c => c.Translations)
                    .FirstOrDefault(w => w.NormalizedWord == Utility.NormalizeWord(searchedWord));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.


                return alreadyExistsWord as T;
            }
            else if (typeof(T) == typeof(Turkish))
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Turkish alreadyExistsWord = words.OfType<Turkish>().Include(c => c.Categories).Include(c => c.Translations)
                    .FirstOrDefault(w => w.NormalizedWord == Utility.NormalizeWord(searchedWord));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                return alreadyExistsWord as T;
            }

            return null;
        }
    }
}

