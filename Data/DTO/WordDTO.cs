using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class WordDTO
    {
        public int Id { get; set; }
        public string? English { get; set; }
        public string? Turkish { get; set; }
        public string[]? Categories { get; set; }
    }
}
