using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAPI.Core.Models
{
    [Table("Turkishes")]
    public class Turkish : AWord, IWord
    {
        public virtual ICollection<Category>? Categories { get; set; }
        public new ICollection<English> Translations { get; set; }
        = new List<English>();

        public override List<string> getCategories()
        {
            return this.Categories.Select(p => p.Name).ToList();
        }

        public override List<string> getTranslations()
        {
            return this.Translations.Where(z=>z.Status > 0).Select(p => p.NormalizedWord).ToList();
        }

        ICollection<T> IWord.Translations<T>()
        {
            return Translations.Cast<T>().ToList();
        }

    }
}
