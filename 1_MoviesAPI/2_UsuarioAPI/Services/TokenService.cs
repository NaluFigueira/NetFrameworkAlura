using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _2_UsuarioAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace _2_UsuarioAPI.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> user)
        {
            Claim[] userRights = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdpajsdpoajdojcojopajcm"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: userRights,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(tokenString);
        }
    }
}
