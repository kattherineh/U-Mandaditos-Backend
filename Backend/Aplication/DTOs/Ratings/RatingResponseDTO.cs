namespace Aplication.DTOs
{
    public class RatingResponseDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string ProfilePic { get; set; } = string.Empty;

        public int Score { get; set; }

        public string Review { get; set; } = string.Empty;

        public DateTime DatePosted { get; set; }

        public bool isRunner { get; set; }
        
    }
}