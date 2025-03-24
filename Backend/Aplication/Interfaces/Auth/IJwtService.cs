using Aplication.DTOs.Auth.Jwt;
using Domain.Entities;

namespace Aplication.Interfaces.Auth
{
    public interface IJwtService
    {
        JwtResponseDTO GenerateToken(User user);
    }
}
