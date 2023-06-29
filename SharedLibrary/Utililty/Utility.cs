using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SharedLibrary.Exceptions;
using WordsAPI.SharedLibrary.Exceptions;

namespace SharedLibrary.Utililty
{
    public static class Utility
    {
        private readonly static IConfiguration _configuration;

        static Utility()
        {
            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            _configuration = configurationBuilder.Build();
        }

        public static string NormalizeWord(string? input)
        {
            string normalized = (input ?? "").Normalize(NormalizationForm.FormD);
            normalized = new string(normalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            normalized = normalized.Normalize(NormalizationForm.FormKC);

            return normalized.ToUpperInvariant();
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T isNull<T>(T property)
        {
            if (property == null)
            {
                var errorMessageSection = _configuration.GetSection("ErrorMessages").GetSection("NotFound");

                throw new NotFoundException(errorMessageSection[typeof(T).Name] ?? "Veri bulunamadı");
            }

             return property;
        }
    }
}
