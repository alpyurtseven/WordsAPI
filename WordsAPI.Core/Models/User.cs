using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAPI.Core.Models
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? NormalizedUsername { get; set; }
        public string? NormalizedEmail { get; set; }
        public string? NormalizedPassword { get; set; }
        public string? ProfilePicture { get; set; }
        public int Level { get; set; }
        public float ExperiencePoints { get; set; }
        public float RequiredExcperincePoints { get; set; }
        public byte Type { get; set; }
        public byte Status { get; set; }
        public ICollection<UserWord> UserWords { get; set; }

    }
}
