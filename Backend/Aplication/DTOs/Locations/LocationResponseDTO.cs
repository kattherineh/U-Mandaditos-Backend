namespace Aplication.DTOs.Locations;

public class LocationResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool Active { get; set; }
}