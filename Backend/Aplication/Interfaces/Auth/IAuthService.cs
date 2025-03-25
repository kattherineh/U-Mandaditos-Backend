using Aplication.DTOs.Auth.Login;
using Aplication.DTOs.General;

namespace Aplication.Interfaces.Auth
{
    public interface IAuthService
    {
       Task<ResponseDTO<LoginResponseDTO>> Login(LoginRequestDTO login);
    }
}
