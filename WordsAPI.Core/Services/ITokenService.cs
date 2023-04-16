using WordsAPI.Core.Configuration;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;

namespace WordsAPI.Core.Services
{
    public interface ITokenService
    {
        TokenDTO CreateToken(User user);
        ClientTokenDTO CreateTokenByClient(Client client);
    }
}
