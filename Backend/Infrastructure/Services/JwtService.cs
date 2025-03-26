using Aplication.DTOs.Auth.Jwt;
using Aplication.Interfaces.Helpers;
using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtService: IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtResponseDTO GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("IdUser", user.Id.ToString()),
                new Claim("DNI", user.Dni)
            };

            var key = _jwtSettings.SecretKey;
            var expirationMinutes = _jwtSettings.ExpirationMinutes;

            var tokenHandler = new JwtSecurityTokenHandler();
            var byteKey = Encoding.UTF8.GetBytes(key);

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(byteKey),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDes);

            var tokenR = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtResponseDTO(tokenR, (DateTime)tokenDes.Expires);
        }
    }
}
