using Domain.Entities;

namespace Aplication.DTOs {
    public class OfferResponseDTO {
        public int Id { get; set; }

        public double CounterOfferAmount { get; set; }

        public User? UserCreator { get; set; }

        public Post? Post { get; set; }

        public bool IsCounterOffer { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}