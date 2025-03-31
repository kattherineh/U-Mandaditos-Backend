using Aplication.Interfaces.Auth;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedUserService(IHttpContextAccessor contextAccessor)
    {
        _httpContextAccessor = contextAccessor;
    }
    
    public int GetAuthenticatedUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User; //Obtiene el User
        var idClaim = user?.FindFirst("IdUser")?.Value; //Obtiene el idUser

        if (int.TryParse(idClaim, out int userId)) // Intenta convertir el idClaim a int
        {
            return userId;
        }

        return 0; // Devuelve 0 si no se pudo convertir
    }
}