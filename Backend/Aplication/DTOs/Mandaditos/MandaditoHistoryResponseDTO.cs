namespace Aplication.DTOs.Mandaditos;

public class MandaditoHistoryResponseDTO
{
    public int Id { get; set; }
    public string? PickupLocationName { get; set; } = string.Empty;
    public string? DeliveryLocationName { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string? RunnerName { get; set; } = string.Empty;
    public DateTime? DeliveredAt { get; set; }
}