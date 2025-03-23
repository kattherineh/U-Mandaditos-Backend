using Domain.Common;

namespace Domain.Entities
{
    public class Rating: Entity
    {
        public int IdMandadito { get; set; }
        public Mandadito? Mandadito { get; set; }

        public int IdRater { get; set; }
        public User? RaterUser { get; set; }

        public int IdRatedUser { get; set; }
        public User? RatedUser { get; set; }

        public int IdRatedRole { get; set; }
        public UserRole? RatedRole { get; set; }

        public int RatingNum { get; set; }

        public string Review { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}