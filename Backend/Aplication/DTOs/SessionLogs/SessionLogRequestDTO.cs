namespace Aplication.DTOs.SessionLogs
{
    public class SessionLogRequestDTO
    {
        public string? IpAddress { get; set; }
        public string? DeviceInfo { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int UserId { get; set; }
    }
}
