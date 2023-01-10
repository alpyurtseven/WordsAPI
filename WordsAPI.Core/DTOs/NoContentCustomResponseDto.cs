using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WordsAPI.Core.DTOs
{
    public class NoContentCustomResponseDto
    {
        public List<string> Errors { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}
