using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsAPI.Core.Models
{
    public class UserWord
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int WordId { get; set; }
        public DateTime LastCorrectAnswerDate { get; set; }
        public int WrongAnswersCount { get; set; }
        public int CorrectAnswersCount { get; set; } 
        public User User { get; set; }
        public English Word { get; set; }
    }
}
