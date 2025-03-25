namespace Aplication.DTOs
{
    public class RatingRequestDTO
    {
        public int IdRater { get; set; }

        public int IdRatedUser { get; set; }

        public int RatingNum { get; set; }

        public string Review { get; set; } = string.Empty;

        public int IdRatedRole { get; set; }

    }
}