namespace Aplication.DTOs.Auth.Login
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public string? Token { get; set; }
        public DateTime Expiration { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }

    }
}
