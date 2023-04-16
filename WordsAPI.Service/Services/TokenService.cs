using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.Configuration;
using WordsAPI.Core.DTOs;
using WordsAPI.Core.Models;
using WordsAPI.Core.Services;

namespace WordsAPI.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IService<User> _userService;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(IService<User> userService, IOptions<CustomTokenOption> options) { 
            _userService= userService;
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
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.UserData,user.Name),
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
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }

        public ClientTokenDTO CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _tokenOption.Issuer, expires: accessTokenExpiration, notBefore: DateTime.Now, claims: GetClaimsByClient(client));

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
