using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAPI.Core.Models
{
    public interface IWord
    {
        public int Id { get; set; }
        public string? Word { get; set; }
        public string? NormalizedWord { get; set; }
        public byte Status { get; set; }
        ICollection<T> Translations<T>() where T : IWord;
        public ICollection<Category>? Categories { get; set; }
    }
}
