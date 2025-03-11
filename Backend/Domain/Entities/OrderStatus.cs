using Domain.Common;

namespace Domain.Entities;

public class OrderStatus : EntityCatalog
{
    public bool Active { get; set; }
}