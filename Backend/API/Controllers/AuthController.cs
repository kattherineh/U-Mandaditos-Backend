using Aplication.DTOs.Auth.Login;
using Aplication.DTOs.SessionLogs;
using Aplication.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDTO)
        {
            // Registros de depuración
            Console.WriteLine($"Email recibido: {loginDTO.Email}");
            Console.WriteLine($"Contraseña recibida: {loginDTO.Password}");

            if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                return BadRequest("El correo y la contraseña son requeridos.");
            }

            var loginInfo = await _authService.Login(loginDTO);
            return loginInfo is null ? Unauthorized("Credenciales inválidas") : Ok(loginInfo);
        }

    }
}
