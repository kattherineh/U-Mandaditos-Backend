namespace Aplication.DTOs.Mandaditos;

public class MandaditoHistoryDto
{
    public Dictionary<string, List<MandaditoDetailDto>> History { get; set; } = new();
}

public class MandaditoDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double AcceptedRate { get; set; }
    public string Status => DeliveredAt.HasValue ? "Delivered" : "In Progress";
    public DateTime AcceptedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string SecurityCode { get; set; } = string.Empty;
    public LocationDto? PickupLocation { get; set; }
    public LocationDto? DeliveryLocation { get; set; }
}

public class LocationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}