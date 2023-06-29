using System.ComponentModel.DataAnnotations.Schema;

namespace WordsAPI.Core.Models
{
    [Table("Turkishes")]
    public class Turkish : AWord, IWord
    {
        public override ICollection<Category>? Categories { get; set; } = new List<Category>();
        public new ICollection<English> Translations { get; set; } = new List<English>();

        public override List<string> getCategories()
        {
            return Categories.Select(p => p.Name)
                .ToList();
        }

        public override List<string> getTranslations()
        {
            return Translations.Where(z => z.Status > 0)
                .Select(p => p.Word)
                .ToList();
        }

        ICollection<T> IWord.Translations<T>()
        {
            return Translations.Cast<T>().ToList();
        }

    }
}
