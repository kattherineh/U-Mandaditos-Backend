using Domain.Common;

namespace Domain.Entities;

public class Post: AggregateRoot
{
    public string Description { get; set; }

    public double SugestedValue { get; set; }
    
    public int? IdPosterUser { get; set; }
    public User PosterUser { get; set; }
    
    public int? IdPickUpLocation { get; set; }
    public Location PickUpLocation { get; set; }
    
    public int?  IdDeliveryLocation { get; set; }
    public  Location DeliveryLocation { get; set; }
    
    public  DateTime CreatedAt { get; set; }
    
    public bool Completed { get; set; }
    
}