using Aplication.Interfaces.SessionLogs;

namespace Aplication.Services
{
    internal class SessionLogService : ISessionLogService
    {
        private readonly ISessionLogService _sessionLogService;

        public SessionLogService(ISessionLogService sessionLogService)
        {
            _sessionLogService = sessionLogService;
        }

        public Task<bool> ValidateSessionToken(string token)
        {
            return _sessionLogService.ValidateSessionToken(token);
        }
    }
}
