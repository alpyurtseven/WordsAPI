using System;
namespace WordsAPI.SharedLibrary.Exceptions
{
	public class ClientSideException:Exception
	{
		public ClientSideException(string message): base(message)
		{

		}
	}
}

