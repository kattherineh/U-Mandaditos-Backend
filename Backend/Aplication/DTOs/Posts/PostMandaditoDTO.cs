using Aplication.DTOs.Locations;
using Aplication.DTOs.Users;

namespace Aplication.DTOs.Posts;
/**
 * Esta clase es un DTO que se utiliza para representar un post de un mandadito.
 */
public class PostMandaditoDTO
{
    public int Id { get; set; }
    public double SuggestedValue { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserResponseMandaditoDTO? PosterUser { get; set; }
    public string? PickupLocation { get; set; }
    public string? DeliveryLocation { get; set; }
}