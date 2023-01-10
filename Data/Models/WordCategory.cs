using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class WordCategory
    {
        public int EnglishId { get; set; }
        public English? English { get; set; }
        public int TurkishId { get; set; }
        public Turkish? Turkish { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
