using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Repositories;
using WordsAPI.Core.Services;
using WordsAPI.Core.UnitOfWorks;
using WordsAPI.Repository.Repositories;

namespace WordsAPI.Service.Services
{
    public class WordService<T> : Service<T>, IWordService<T> where T : AWord 
    {
        private readonly IWordRepository<T> _wordRepository;
        private readonly IService<Turkish> _turkishService;
        private readonly IService<Category> _categoryService;
        private readonly IService<English> _englishService;

        public WordService(IGenericRepository<T> repository, IUnitOfWork unitOfWork,IWordRepository<T> wordRepository, IService<Turkish> turkishService, IService<English> englishService, IService<Category> category) : base(repository, unitOfWork)
        {
            _wordRepository = wordRepository;
            _turkishService= turkishService;
            _categoryService = category;
            _englishService= englishService;
        }

        public async Task<CustomResponseDto<List<WordDTO>>> GetWordsWithRelations()
        {
            List<WordDTO> wordsDto = new List<WordDTO>();

            var words = await _wordRepository.GetWordsWithRelations();

            foreach (var item in words)
            {
                 wordsDto.Add(new WordDTO() { Word = item.NormalizedWord, Translations = item.getTranslations(), Categories = 
                    item.getCategories() });
            }

            return CustomResponseDto<List<WordDTO>>.Success(200, wordsDto);
        }

        public async Task<CustomResponseDto<WordDTO>> GetWordWithRelations(int id)
        {
            var word = await _wordRepository.GetWordWithRelations(id);

            return CustomResponseDto<WordDTO>.Success(200, new WordDTO() { Word = word.NormalizedWord, Translations = word.getTranslations(), Categories = word.getCategories() });
        }

        public async Task<CustomResponseDto<WordDTO>> Save(WordDTO word)
        {
            var categories = await GetOrCreateItemCategory(word.Categories);
            var translations = await GetOrCreateTranslations(word.Translations);

            if (typeof(T) == typeof(English))
            {
                 var allEnglishWords = await _englishService.GetAllAsync();
                 var matchedEnglishEntity = allEnglishWords.Where(w => w.NormalizedWord == word.Word.ToUpper()).FirstOrDefault();
                 var alreadyExistsEnglishWord = GetExistEntity<English>(allEnglishWords, word.Word);
                 var newEnglish = new English() { Word = word.Word, NormalizedWord = word.Word.ToUpper(), Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Categories = new List<Category>(), Translations = new List<Turkish>() };


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
                                newEnglish.Categories.Add(existingCategory);
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
                    if (translations.TryGetValue(translation.ToUpper(), out var existingtranslation))
                    {
                        if (alreadyExistsEnglishWord != null && alreadyExistsEnglishWord.Translations != null)
                        {
                            if (existingtranslation is Turkish turkishWord)
                            {
                                var existTranslation = alreadyExistsEnglishWord.Translations.FirstOrDefault(c => c.Id == existingtranslation.Id);
                                if (existTranslation == null)
                                {
                                    foreach (var category in word.Categories)
                                    {
                                        if (categories.TryGetValue(category, out var existingCategory))
                                        {
                                            var existCategory = alreadyExistsEnglishWord.Categories.FirstOrDefault(c => c.Id == existingCategory.Id);
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
                            foreach (var category in word.Categories)
                            {
                                if (categories.TryGetValue(category, out var existingCategory))
                                {
                                    existingtranslation.Categories.Add(existingCategory);
                                }
                            }

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
                var matchedTurkishEntity = allTurkishWords.Where(w => w.NormalizedWord == word.Word.ToUpper()).FirstOrDefault();
                var alreadyExistsTurkishWord = GetExistEntity<Turkish>(allTurkishWords, word.Word);
                var newTurkish = new Turkish() { Word = word.Word, NormalizedWord = word.Word.ToUpper(), Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Categories = new List<Category>(), Translations = new List<English>() };


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
                                newTurkish.Categories.Add(existingCategory);
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
                    if (translations.TryGetValue(translation.ToUpper(), out var existingtranslation))
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
                                        if (categories.TryGetValue(category, out var existingCategory))
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
      
            return CustomResponseDto<WordDTO>.Success(201, word);
        }

        public async Task<Dictionary<string, Category>> GetOrCreateItemCategory(List<string> categories)
        {
            var allCategories = await _categoryService.GetAllAsync();
            var existingCategories = allCategories.Where(c => categories.Select(nc => nc).Contains(c.Name)).ToDictionary(c => c.Name, c => c);

            foreach (var category in categories)
            {
                var matchedCategory = allCategories.SingleOrDefault(z => z.Name.ToUpper() == category.ToUpper());

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
            var existingTranslations = allTranslations.Where(c => translations.Select(nc => nc).Contains(c.NormalizedWord)).ToDictionary(c => c.NormalizedWord, c => (IWord)c);

            foreach (var translation in translations)
            {
                var matchedTranslation = allTranslations.SingleOrDefault(z => z.NormalizedWord == translation.ToUpper());

                if (matchedTranslation == null)
                {
                    if (typeof(T) == typeof(English))
                    {
                        Turkish newTurkish = new Turkish() { Word = translation, NormalizedWord = translation.ToUpper(), Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Categories = new List<Category>() };
                        await _turkishService.AddAsync(newTurkish);

                        existingTranslations.Add(newTurkish.NormalizedWord, newTurkish);
                    } else {
                        English newEnglish = new English() { Word = translation, NormalizedWord = translation.ToUpper(), Status = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now , Categories = new List<Category>() };
                        await _englishService.AddAsync(newEnglish);

                        existingTranslations.Add(newEnglish.NormalizedWord, newEnglish);
                    }
                }
            }

            return existingTranslations;
        }

        public T GetExistEntity<T>(IQueryable<IWord> words, string searchedWord) where T : class, IWord
        {
            if (typeof(T) == typeof(English))
            {
                English alreadyExistsWord = words.OfType<English>().Include(c=>c.Categories).Include(c=>c.Translations)
                    .FirstOrDefault(w => w.NormalizedWord == searchedWord.ToUpper());


                return alreadyExistsWord as T;
            }
            else if (typeof(T) == typeof(Turkish))
            {
                Turkish alreadyExistsWord = words.OfType<Turkish>().Include(c => c.Categories).Include(c => c.Translations)
                    .FirstOrDefault(w => w.NormalizedWord == searchedWord.ToUpper());

                return alreadyExistsWord as T;
            }

            return null;
        }

        public async Task<CustomResponseDto<WordDTO>> Delete(int id)
        {
            if (typeof(T) == typeof(English))
            {
                var word = await _englishService.GetByIdAsync(id);

                word.Status = 0;

                await _englishService.UpdateAsync(word);

               return CustomResponseDto<WordDTO>.Success(201, new WordDTO() { Word = word.NormalizedWord});
            }
            else
            {
                var word = await _turkishService.GetByIdAsync(id);

                word.Status = 0;

                await _turkishService.UpdateAsync(word);

                return CustomResponseDto<WordDTO>.Success(201, new WordDTO() { Word = word.NormalizedWord });
            }
        }
    }
}
