namespace Aplication.Interfaces.SessionLogs
{
    public interface ISessionLogService
    {
        Task<bool> ValidateSessionToken(string token);
    }
}
