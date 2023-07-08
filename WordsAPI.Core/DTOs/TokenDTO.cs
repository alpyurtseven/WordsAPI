﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WordsAPI.Core.DTOs
{
    public class TokenDTO
    {
        public string? AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public UserDTO User { get; set; }
    }
}
