using Aplication.DTOs.Auth.Jwt;
using Domain.Entities;

namespace Aplication.Interfaces.Helpers
{
    public interface IJwtService
    {
        JwtResponseDTO GenerateToken(User user);
    }
}
