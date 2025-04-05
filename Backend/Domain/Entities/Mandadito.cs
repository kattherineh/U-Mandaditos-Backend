using Domain.Common;

namespace Domain.Entities
{
    public class Mandadito: Entity
    {
        public string SecurityCode = string.Empty;
        public double AcceptedRate;

        public int IdPost { get; set; }
        public Post? Post { get; set; }

        public int IdOffer { get; set; }
        public Offer? Offer { get; set; }
        
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public DateTime AcceptedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        
        public Mandadito(string securityCode, double acceptedRate, int idPost, int idOffer, DateTime acceptedAt)
        {
            SecurityCode = securityCode;
            AcceptedRate = acceptedRate;
            IdPost = idPost;
            IdOffer = idOffer;
            AcceptedAt = acceptedAt;
        }
    }
}
