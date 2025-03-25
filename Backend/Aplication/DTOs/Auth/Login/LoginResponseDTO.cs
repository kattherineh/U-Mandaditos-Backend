namespace Aplication.DTOs.Auth.Login
{
    public class LoginResponseDTO
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }

    }
}
