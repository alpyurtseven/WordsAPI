    using System.Globalization;
    using System.Text;
    using Microsoft.Extensions.Configuration;
    using SharedLibrary.Exceptions;

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
            if (string.IsNullOrEmpty(input))
                return input;

            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    result.Append(c);
            }

            return result.ToString().ToLowerInvariant();
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
