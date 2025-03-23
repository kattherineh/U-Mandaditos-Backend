namespace Aplication.DTOs.Posts;

public class PostReponseDTO
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public double SuggestedValue { get; set; }
    public string PosterUserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}