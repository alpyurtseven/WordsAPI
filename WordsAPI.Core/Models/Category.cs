using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAPI.Core.Models
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<English>? Englishes { get; set; }
        public ICollection<Turkish>? Turkishes { get; set; }

    }
}
