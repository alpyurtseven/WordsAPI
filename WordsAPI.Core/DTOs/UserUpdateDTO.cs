using System;
namespace WordsAPI.Core.DTOs
{
	public class UserUpdateDTO
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string? Password { get; set; }
		public string ProfilePicture { get; set; }
	}
}

