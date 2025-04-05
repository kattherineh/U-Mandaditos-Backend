namespace Aplication.DTOs.Posts;

public class PostRequestDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double SuggestedValue { get; set; }
    public int IdPickUpLocation { get; set; }
    public int IdDeliveryLocation { get; set; }
}