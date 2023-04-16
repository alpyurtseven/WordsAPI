using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WordsAPI.Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = "defaultprofilepicture.png";
        public int Level { get; set; } = 0;
        public float ExperiencePoints { get; set; } = 0;
        public float RequiredExperiencePoints { get; set; } = int.MaxValue;
        public System.DateTimeOffset CreatedAt { get; set; } = System.DateTimeOffset.UtcNow;
        public ICollection<UserWord> UserWords { get; set; } = new List<UserWord>();

    }
}
