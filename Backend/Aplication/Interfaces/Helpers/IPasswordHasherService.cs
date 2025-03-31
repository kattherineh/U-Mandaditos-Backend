namespace Aplication.Interfaces.Helpers;

public interface IPasswordHasherService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
}