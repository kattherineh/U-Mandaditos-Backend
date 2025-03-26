using Microsoft.AspNetCore.Http;

namespace Aplication.DTOs.Users
{
    public class UserProfileRequestDTO
    {
        public string? Name { get; set; }
        public string? Dni { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }   

        public string? Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public int Score { get; set; }

        public IFormFile? ProfilePic { get; set; }

        public int IdLastLocation { get; set; }
        public int IdCareer { get; set; }
    }
}
