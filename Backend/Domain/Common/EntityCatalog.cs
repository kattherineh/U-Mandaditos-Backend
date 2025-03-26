namespace Domain.Common;

public abstract class EntityCatalog
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
}