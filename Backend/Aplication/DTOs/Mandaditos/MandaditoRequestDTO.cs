namespace Aplication.DTOs.Mandaditos;

public class MandaditoRequestDTO
{
    public int PostId { get; set; }
    public int OfferId { get; set; }
    public double AcceptedRate { get; set; }
    public DateTime AcceptedAt { get; set; }
}