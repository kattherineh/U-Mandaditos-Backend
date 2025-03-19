using Domain.Common;

namespace Domain.Entities;

public class SessionLog : Entity
{
    public string IpAddress { get; set; } = string.Empty;
    public string DeviceInfo { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
}