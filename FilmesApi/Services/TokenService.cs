

using FilmesApi.Data.Dtos.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilmesApi.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> Usuario, string role)
        {
            Claim[] direitosUsuarios = new Claim[]
            {
                new Claim("username", Usuario.UserName),
                new Claim("id", Usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("MinhaChave12345678MinhaChave12345678")
            );

            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: direitosUsuarios,
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}
