using Domain.Common;

namespace Domain.Entities
{
    public class Location: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Active { get; set; }

        public Location(string name, string description, decimal latitude, decimal longitude, bool active)
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            Active = active;
        }
    }
}
