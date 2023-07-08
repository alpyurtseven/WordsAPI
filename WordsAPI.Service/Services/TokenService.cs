using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WordsAPI.Core.Configuration;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Services;

namespace WordsAPI.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly CustomTokenOption _tokenOption;

        public TokenService(IOptions<CustomTokenOption> options) { 
            _tokenOption = options.Value;
        }

        private string CreateRefreshToken() {
            var numberByte = new Byte[32];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }
        
        private IEnumerable<Claim> GetClaims(User user, List<string> audiences)
        {
            var userList = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.UserData,user.FirstName),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            userList.AddRange(audiences.Select(z => new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Aud, z)));

            return userList;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim> {
                 new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub,client.Id.ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            claims.AddRange(client.Audiences.Select(z => new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Aud, z)));

            return claims;
        }

        public TokenDTO CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _tokenOption.Issuer, expires: accessTokenExpiration, notBefore: DateTime.Now, claims: GetClaims(user, _tokenOption.Audience),
                signingCredentials:signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new TokenDTO
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration= accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,
                User = new UserDTO()
                {
                    Username= user.UserName,
                    Email = user.Email,
                    ExperiencePoint = user.ExperiencePoints,
                    RequiredExperiencePoint = user.RequiredExperiencePoints,
                    ProfilePicture = user.ProfilePicture,
                    Surname = user.LastName,
                    Name = user.FirstName,
                    Id = user.Id,
                    Level = user.Level
                }
            };

            return tokenDto;
        }

        public ClientTokenDTO CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken( issuer: _tokenOption.Issuer, expires: accessTokenExpiration, notBefore: DateTime.Now, claims: GetClaimsByClient(client), signingCredentials :signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            var tokenDto = new ClientTokenDTO
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
            };

            return tokenDto;
        }
    }
}
