using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class English
    {
        public int Id { get; set; }
        public string? Word { get; set; }
        public string? NormalizedWord { get; set; }
        public byte Type { get; set; }
        public byte Status { get; set; }
        public ICollection<WordCategory>? WordCategories { get; set; }
    }
}
