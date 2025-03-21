using Domain.Common;

namespace Domain.Entities
{
    public class Mandadito: Entity
    {
        public string SecurityCode = string.Empty;
        public decimal AcceptedRate;

        public int IdPost { get; set; }
        public Post? Post { get; set; }

        public int IdOffer { get; set; }
        public Offer? Offer { get; set; }

        public DateTime AcceptedAt { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
}
