using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SharedLibrary.Dtos;
using SharedLibrary.Utililty;
using WordsAPI.Core.Configuration;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Repositories;
using WordsAPI.Core.Services;
using WordsAPI.Core.UnitOfWorks;
using WordsAPI.SharedLibrary.Exceptions;

namespace WordsAPI.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, IUserRepository userRepository, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenRespository, IConfiguration configuration)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userRefreshTokenRepository = userRefreshTokenRespository;
            _configuration = configuration;
        }


        public async Task<CustomResponseDto<TokenDTO>> CreateTokenAsync(UserLoginDTO loginDTO)
        {
            var user = Utility.isNull(await _userRepository.GetUserByEmailAsync(loginDTO.Email));

            if (await _userRepository.CheckPasswordAsync(user, loginDTO.Password))
            {
                var token = _tokenService.CreateToken(user);
                var userRefreshToken = await _userRefreshTokenRepository.Where(z=>z.UserId == user.Id.ToString()).SingleOrDefaultAsync();

                if (userRefreshToken == null)
                {
                    await _userRefreshTokenRepository.AddAsync(new UserRefreshToken() {
                        Code=token.RefreshToken,
                        Expiration=token.RefreshTokenExpiration,
                        UserId=user.Id.ToString()});


                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    userRefreshToken.Code = token.RefreshToken;
                    userRefreshToken.Expiration = token.RefreshTokenExpiration;

                    _userRefreshTokenRepository.Update(userRefreshToken);

                    await _unitOfWork.CommitAsync();
                }

                return CustomResponseDto<TokenDTO>.Success(200, token);
            }

            throw new ClientSideException("Kullanıcı adı ya da şifre hatalı");
        }

        public CustomResponseDto<ClientTokenDTO> CreateTokenByClient(ClientLoginDTO clientLogin)
        {
            var client = Utility.isNull<Client>(_clients
                .SingleOrDefault(z => z.Id == clientLogin.Id && z.Secret == clientLogin.Secret));

            var token = _tokenService.CreateTokenByClient(client);

            return CustomResponseDto<ClientTokenDTO>.Success(200, token);
        }

        public async Task<CustomResponseDto<TokenDTO>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = Utility.isNull<UserRefreshToken>(await _userRefreshTokenRepository.Where(z => z.Code == refreshToken).SingleOrDefaultAsync());
            var user = await _userRepository.GetUserById(existRefreshToken.UserId);
            var token = _tokenService.CreateToken(user);

            existRefreshToken.Code = token.RefreshToken;
            existRefreshToken.Expiration = token.RefreshTokenExpiration;

            _userRefreshTokenRepository.Update(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDTO>.Success(200, token);
        }

        public async Task<CustomResponseDto<TokenDTO>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = Utility.isNull<UserRefreshToken>(await _userRefreshTokenRepository.Where(z => z.Code == refreshToken).SingleOrDefaultAsync());

            _userRefreshTokenRepository.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDTO>.Success(200,new TokenDTO());
        }
    }
}
