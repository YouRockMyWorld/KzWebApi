using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using KZ.API.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KZ.API.Authentication
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IOptions<JwtTokenConfig> _options;

        public TokenHelper(IOptions<JwtTokenConfig> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
        public string CreateToken(LoginDataDto loginData)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Role, loginData.Username)
            };
            return CreateToken(claims);
        }

        private string CreateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_options.Value.AccessTokenExpiresMinutes),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
