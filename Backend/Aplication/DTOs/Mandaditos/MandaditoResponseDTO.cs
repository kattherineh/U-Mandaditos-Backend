using Aplication.DTOs.Offers;
using Aplication.DTOs.Posts;

namespace Aplication.DTOs.Mandaditos;

public class MandaditoResponseDTO
{
    public int Id { get; set; }
    public string SecurityCode { get; set; } = string.Empty;
    public DateTime AcceptedAt { get; set; }
    public double AcceptedRate { get; set; }
    public OfferDTO? Offer { get; set; }
    public PostMandaditoDTO? Post { get; set; }
    public List<RatingResponseDTO?> Ratings { get; set; } = new List<RatingResponseDTO?>();
}