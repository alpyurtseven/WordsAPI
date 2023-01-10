using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAPI.Core.Models
{
    public class User : BaseEntity
    {
        public string? FullName { get; set; }

        public string? Mail { get; set; }

        public string? Password { get; set; }

        public byte Type { get; set; }

        public byte Status { get; set; }
    }
}
