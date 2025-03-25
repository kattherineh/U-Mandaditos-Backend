namespace Aplication.DTOs.Media;

public class MediaRequestDTO
{
    public string Name { get; set; }
    public string Link { get; set; }
    
    public Stream fileStream  { get; set; }
}