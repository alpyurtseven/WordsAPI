using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.DTOs
{
    public class WordDTO
    {
        public int Id { get; set; }
        public List<string> English { get; set; }
        public List<string> Turkish { get; set; }
        public List<string> NormalizedEnglish { get; set; }
        public List<string> NormalizedTurkish { get; set; }
        public List<Category> Categories { get; set; }
    }
}
