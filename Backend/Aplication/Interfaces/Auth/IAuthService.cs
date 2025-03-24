using Aplication.DTOs.Auth.Login;

namespace Aplication.Interfaces.Auth
{
    public interface IAuthService
    {
       Task<LoginResponseDTO> Login(LoginRequestDTO login);
    }
}
