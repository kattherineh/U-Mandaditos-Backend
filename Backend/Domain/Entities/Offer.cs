using Domain.Common;

namespace Domain.Entities
{
    public class Offer: Entity
    {
        public double CounterOfferAmount {  get; set; }

        public int IdPost {  get; set; }
        public Post? Post { get; set; }

        public int IdUserCreator {  get; set; }
        public User? UserCreator { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsCounterOffer { get; set; }
        public bool Accepted {  get; set; }
    }
}
