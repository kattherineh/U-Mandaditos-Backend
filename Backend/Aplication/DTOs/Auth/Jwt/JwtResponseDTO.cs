namespace Aplication.DTOs.Auth.Jwt
{
    public class JwtResponseDTO
    {
        public string TokenR { get; }
        public DateTime Expires { get; }

        public JwtResponseDTO(string tokenR, DateTime expires)
        {
            TokenR = tokenR;
            Expires = expires;
        }
    }
}
