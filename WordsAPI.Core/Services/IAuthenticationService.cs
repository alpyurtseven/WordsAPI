using SharedLibrary.Dtos;
using WordsAPI.Core.DTOs;

namespace WordsAPI.Core.Services
{
    public interface IAuthenticationService
    {
        Task<CustomResponseDto<TokenDTO>> CreateTokenAsync(UserLoginDTO loginDTO);
        Task<CustomResponseDto<TokenDTO>> CreateTokenByRefreshToken(string refreshToken);
        Task<CustomResponseDto<TokenDTO>> RevokeRefreshToken(string refreshToken);
        CustomResponseDto<ClientTokenDTO> CreateTokenByClient(ClientLoginDTO clientLogin);
    }
}
