using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.DTOs
{
    public  class WordDTO
    {
        public string Word { get; set; }
        public List<string> Translations { get; set; }
        public List<string> Categories { get; set; }
    }
}
