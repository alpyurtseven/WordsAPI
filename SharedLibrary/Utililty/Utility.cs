using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Utililty
{
    public static class Utility
    {
        public static string NormalizeWord(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
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
    }
}
