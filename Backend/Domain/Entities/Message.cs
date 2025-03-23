using Domain.Common;

namespace Domain.Entities
{
    public class Message : Entity
    {
        public int IdUser { get; set; }
        public User? User { get; set; }

        public int IdMandadito { get; set; }
        public Mandadito? Mandadito { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}