using Domain.Common;

namespace Domain.Entities
{
    public class OrderStatusHistory: Entity
    {
        public int IdMandadito { get; set; }
        public Mandadito? Mandadito { get; set; }

        public int IdStatus { get; set; }
        public OrderStatus? OrderStatus { get; set; }

        public DateTime ChangeAt { get; set; }

        public bool Active { get; set; }
    }
}