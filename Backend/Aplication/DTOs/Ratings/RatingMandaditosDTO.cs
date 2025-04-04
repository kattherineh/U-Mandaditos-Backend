namespace Aplication.DTOs;

public class RatingMandaditosDTO
{
    public string Review { get; set; } = string.Empty;
    public string DatePosted { get; set; } = string.Empty;
    public int Rating { get; set; }
    public bool IsRunner { get; set; } 

}