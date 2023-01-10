using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAPI.Core.Models
{
    public class English : BaseEntity
    {
        public string? Word { get; set; }
        public string? NormalizedWord { get; set; }
        public byte Type { get; set; }
        public byte Status { get; set; }
        public ICollection<WordCategory>? WordCategories { get; set; }
    }
}
