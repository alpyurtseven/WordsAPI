using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAPI.Core.Models
{
    public abstract class AWord : BaseEntity
    {
        public string? Word { get; set; }
        public string? NormalizedWord { get; set; }
        public byte Status { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
        public ICollection<IWord>? Translations;
        public abstract List<string> getTranslations();
        public abstract List<string> getCategories();
    }

}
