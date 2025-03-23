using Domain.Common;

namespace Domain.Entities;

public class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDay { get; set; }
    public int Rating { get; set; }

    public int? ProfilePicId { get; set; }
    public Media? ProfilePic { get; set; }

    public int? LastLocationId { get; set; }
    public Location? LastLocation { get; set; }

    public int? CareerId { get; set; }
    public Career? Career { get; set; }
}
