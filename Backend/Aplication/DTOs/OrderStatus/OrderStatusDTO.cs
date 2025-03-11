namespace Aplication.DTOs;

public class OrderStatusDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
}