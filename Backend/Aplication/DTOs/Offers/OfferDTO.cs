using Aplication.DTOs.Posts;
using Aplication.DTOs.Users;

namespace Aplication.DTOs.Offers;

public class OfferDTO
{
    public int Id { get; set; }
    public double CounterOfferAmount {  get; set; }

    public int IdPost {  get; set; }

    public UserResponseMandaditoDTO? UserCreator { get; set; }

    public string CreatedAt { get; set; }
    public bool IsCounterOffer { get; set; }
    public bool Accepted {  get; set; }
}