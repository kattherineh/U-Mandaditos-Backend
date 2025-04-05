using Microsoft.AspNetCore.Http;

namespace Aplication.DTOs.Users;

public class UserUpdateProfileRequestDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDay { get; set; }
    public IFormFile? ProfilePic { get; set; }
    public int IdCareer { get; set; }
}