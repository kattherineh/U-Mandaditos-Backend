using Domain.Common;

namespace Domain.Entities {

    public class Management : Entity
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public int ManegementRoleId { get; set; }
        public ManagementRole? ManagementRole { get; set; }

        public string Code { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Active { get; set; } = true;
    }
}