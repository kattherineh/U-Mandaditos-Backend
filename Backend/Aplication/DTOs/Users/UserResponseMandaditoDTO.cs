using Aplication.DTOs.Locations;
using Aplication.DTOs.Media;

namespace Aplication.DTOs.Users;

/**
 * Esta clase es un DTO que se utiliza para representar un usuario en la respuesta de un mandadito.
 */
public class UserResponseMandaditoDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? LastLocation { get; set; }
    public string? ProfilePicture { get; set; }
}