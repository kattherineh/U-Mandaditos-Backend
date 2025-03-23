namespace Aplication.DTOs.Mandaditos;

public class MandaditoResponseMinDTO
{
    public int Id { get; set; }
    public string SecurityCode { get; set; } = string.Empty;
    public DateTime AcceptedAt { get; set; }
    public double AcceptedRate { get; set; }
    public int OfferId { get; set; }
    public int PostId { get; set; }
}