using Aplication.DTOs.Users;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Users;
using Domain.Entities;

namespace Aplication.Services;

public class UserService: IUserService
{
    public readonly IUserRepository _userRepository;
    public readonly IFirebaseStorageService _firebaseService;

    public UserService(IUserRepository userRepository,  IFirebaseStorageService firebaseService)
    {
        _userRepository = userRepository;
        _firebaseService = firebaseService;
    }
    
    public async Task<UserResponseDTO> CreateUserAsync(UserRequestDTO userRequest)
    {
        var user = new User();
        var fileName = $"{userRequest.name}-{userRequest.dni}";
        var photo = await _firebaseService.UploadProfilePicture(userRequest.Photo, fileName, "image/jpeg");
        
        user.Name = userRequest.name;
        user.Email = userRequest.email;
        user.Password = userRequest.password;
        user.Dni = userRequest.dni;
        user.Career = new Career
        {
            Active = true,
            Name = "Ingenieria en sistemas"
        };
        user.ProfilePic = new Media(fileName, photo);
        await _userRepository.AddAsync(user);
        
        var userR = new UserResponseDTO
        {
            Name = user.Name,
            Link = user.ProfilePic.Link
        };

        return userR;
    }
}