namespace Aplication.DTOs
{
    public class OrderStatusHistoryResponseDTO
    {
        public int Id { get; set; }
        public string status { get; set; } = string.Empty;
        public int IdMandadito { get; set; }
        public bool Active { get; set; }
    }
}