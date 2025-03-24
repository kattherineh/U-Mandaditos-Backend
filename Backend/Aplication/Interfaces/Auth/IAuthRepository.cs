using Domain.Auth;
using Domain.Entities;

namespace Aplication.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<User?> Login(Login login);
    }
}
