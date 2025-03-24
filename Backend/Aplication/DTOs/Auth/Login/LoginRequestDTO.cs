using Aplication.DTOs.SessionLogs;

namespace Aplication.DTOs.Auth.Login
{
    public class LoginRequestDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? IPAddress { get; set; }
        public string? DeviceInfo { get; set; }
    }
}
