namespace Aplication.DTOs
{
    public class OfferRequestDTO
    {
        public double CounterOfferAmount { get; set; }

        public int UserCreatorId { get; set; }

        public int PostId { get; set; }

        public bool IsCounterOffer { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}