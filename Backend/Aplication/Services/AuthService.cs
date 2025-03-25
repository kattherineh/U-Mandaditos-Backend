using Aplication.DTOs.Auth.Login;
using Aplication.Interfaces.Auth;
using Aplication.Interfaces.SessionLogs;
using Domain.Auth;
using Domain.Entities;
namespace Aplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;
        private readonly ISessionLogRepository _sessionLogRepository;

        public AuthService(IAuthRepository authRepository, IJwtService jwtService, ISessionLogRepository sessionLogRepository)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
            _sessionLogRepository = sessionLogRepository;
        }

        public async Task<LoginResponseDTO?> Login(LoginRequestDTO login)
        {
            try
            {
                if (login.Email == null) throw new ArgumentNullException(nameof(login.Email));
                if (login.Password == null) throw new ArgumentNullException(nameof(login.Password));

                var userInfo = new Login(login.Email, login.Password);
                var loggedUser = await _authRepository.Login(userInfo);

                if (loggedUser is null)
                {
                    return new LoginResponseDTO
                    {
                        Email = null,
                        FullName = null,
                        Token = null,
                        Expiration = DateTime.MinValue,
                        Success = false,
                        Message = "Credenciales erroneas"
                    };
                }

                // Generar token
                var token = _jwtService.GenerateToken(loggedUser);

                // Registrar log d einicio de sesión
                var sessionLog = new SessionLog
                {
                    IpAddress = login.IPAddress ?? throw new ArgumentNullException(nameof(login.IPAddress)),
                    DeviceInfo = login.DeviceInfo ?? throw new ArgumentNullException(nameof(login.DeviceInfo)),
                    StartedAt = DateTime.Now,
                    EndedAt = TimeZoneInfo.ConvertTimeFromUtc( token.Expires, TimeZoneInfo.Local ),
                    UserId = loggedUser.Id,
                    User = loggedUser,
                    AccessToken = token.TokenR
                }; 

                await _sessionLogRepository.AddAsync(sessionLog);

                return new LoginResponseDTO
                {
                    Email = loggedUser.Email,
                    FullName = loggedUser.Name,
                    Token = token.TokenR,
                    Expiration = token.Expires,
                    Success = true,
                    Message = "Inicio de sesión exitoso"
                };
            }
            catch
            {
                return new LoginResponseDTO
                {
                    Email = null,
                    FullName = null,
                    Token = null,
                    Expiration = DateTime.MinValue,
                    Success = false,
                    Message = "Inicio de sesión fallido"
                };
            }
        }
    }
}
