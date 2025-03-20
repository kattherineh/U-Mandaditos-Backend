using Domain.Common;

namespace Domain.Entities;

public class UserRole: EntityCatalog
{
    public bool Active { get; set; }
}