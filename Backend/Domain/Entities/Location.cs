using Domain.Common;

namespace Domain.Entities
{
    public class Location: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Active { get; set; }

        public Location(string name, string description, double latitude, double longitude, bool active)
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            Active = active;
        }
    }
}
