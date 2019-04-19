using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartPlate.API.Models.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartPlate.API.Helpers
{
    public class TokenGenerator
    {
        private readonly IConfiguration _config;

        public TokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(IUser user)
        {
            var claims = new[]
            {
                new Claim("id", user.Id),
                new Claim("name", user.Name)
            };
            var securityKey = _config.GetSection("Security:JwtKey").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}