namespace Aplication.DTOs.Posts;

public class PostResponseDTO
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public double SuggestedValue { get; set; }
    public string PosterUserName { get; set; } = string.Empty;
    public string CreatedAt { get; set; }
    
    public string PickUpLocation { get; set; }
    public string DeliveryLocation { get; set; }
}