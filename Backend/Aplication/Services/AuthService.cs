using Aplication.DTOs.Auth.Login;
using Aplication.DTOs.General;
using Aplication.Interfaces.Auth;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.SessionLogs;
using Aplication.Interfaces.Users;
using Domain.Entities;
namespace Aplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionLogRepository _sessionLogRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasherService _passwordHasherService;

        public AuthService(IUserRepository userRepository, ISessionLogRepository sessionLogRepository, IJwtService jwtService, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _sessionLogRepository = sessionLogRepository;
            _jwtService = jwtService;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<ResponseDTO<LoginResponseDTO>> Login(LoginRequestDTO login)
        {
            try
            {
                if (login.Email == null) throw new ArgumentNullException(nameof(login.Email));
                if (login.Password == null) throw new ArgumentNullException(nameof(login.Password));
                
                var user = await _userRepository.GetByEmailAsync(login.Email);
                var inputHash = _passwordHasherService.HashPassword(login.Password);

                if (user is null)
                {
                    return new ResponseDTO<LoginResponseDTO>
                    {
                        Success = false,
                        Message = $"Error: El usuario con el correo {login.Email} no fue encontrado",
                        Data = null
                    };
                }

                Console.WriteLine($"Contraseña en BD: {user.Password}");
                Console.WriteLine($"Contraseña ingresada: {login.Password}");
                Console.WriteLine($"¿Coinciden?: {user.Password == inputHash}");
                
                if (user.Password != inputHash)
                {
                    return new ResponseDTO<LoginResponseDTO>
                    {
                        Success = false,
                        Message = "Error: La contraseña que has ingresado es incorrecta",
                        Data = null
                    };
                }
                
                // Generar token
                var token = _jwtService.GenerateToken(user);

                // Registrar log de Inicio de Sesión
                var sessionLog = new SessionLog
                {
                    IpAddress = login.IPAddress ?? throw new ArgumentNullException(nameof(login.IPAddress)),
                    DeviceInfo = login.DeviceInfo ?? throw new ArgumentNullException(nameof(login.DeviceInfo)),
                    StartedAt = DateTime.Now,
                    EndedAt = null,
                    UserId = user.Id,
                    User = user
                };

                await _sessionLogRepository.AddAsync(sessionLog);
               
                return new ResponseDTO<LoginResponseDTO>
                {
                    Success = true,
                    Message = "Inicio de sesión exitoso",
                    Data = new LoginResponseDTO
                    {
                        Email = user.Email,
                        FullName = user.Name,
                        Token = token.TokenR,
                        Expiration = token.Expires,
                    }
                };
            }
            catch (Exception ex) // Capturamos la excepción
            {
                Console.WriteLine($"Error en Login: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                
                return new ResponseDTO<LoginResponseDTO>
                {
                    Success = false,
                    Message = $"Inicio de sesión fallido: {ex.Message}", // Mensaje detallado solo en desarrollo
                    Data = null
                };
            }
        }

    }
}
