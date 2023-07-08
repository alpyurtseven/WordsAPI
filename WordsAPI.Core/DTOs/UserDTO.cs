using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAPI.Core.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? ProfilePicture { get; set; }
        public int Level { get; set; }
        public float ExperiencePoint { get; set; }
        public float RequiredExperiencePoint { get; set; }
    }
}
