using System.Security.Cryptography;
using System.Text;
using Aplication.Interfaces.Helpers;

namespace Infrastructure.Services;

public class PasswordHasherService : IPasswordHasherService
{
    /* 
     * This method receives a password and returns a hashed password.
     */
    public string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    /* 
     * This method receives a hashed password and a password and returns a boolean value.
     * This method is used to verify if the password is correct.
     */
    public bool VerifyPassword(string hashedPassword, string password)
    {
        string hashedInput = HashPassword(password);
        return hashedPassword == hashedInput;
    }
}