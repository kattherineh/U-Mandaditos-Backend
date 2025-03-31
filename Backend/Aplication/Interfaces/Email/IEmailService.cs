namespace Aplication.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailCodeAsync(string email, string code);
}