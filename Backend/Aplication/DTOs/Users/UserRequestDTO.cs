using Microsoft.AspNetCore.Http;
namespace Aplication.DTOs.Users;

public class UserRequestDTO
{
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public int career { get; set; }
    public string dni { get; set; }
    public  string phone { get; set; }
    public DateTime birthday { get; set; }
    public IFormFile Photo { get; set; }
}