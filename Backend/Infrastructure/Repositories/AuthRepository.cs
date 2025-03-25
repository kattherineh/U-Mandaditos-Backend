using Aplication.Interfaces.Auth;
using Domain.Auth;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly BackendDbContext _context;

        public AuthRepository(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Login(Login login)
        {

            // Verificar si el usuario existe
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

            if (foundUser == null) 
            {
                return null;
            }

            // Verificar si la contraseña es correcta
            bool passwordIsValid = foundUser.Password == login.Password;

            if( !passwordIsValid )
            {
                return null;
            }    


            return foundUser;
        }

    }
}
