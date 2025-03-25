using Aplication.DTOs.General;
using Aplication.DTOs.Users;
using Aplication.Interfaces;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Users;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Aplication.Services;

public class UserService: IUserService
{
    public readonly IUserRepository _userRepository;
    public readonly IFirebaseStorageService _firebaseService;
    public readonly ICareerRepository _careerRepository;

    public UserService(IUserRepository userRepository,  IFirebaseStorageService firebaseService, ICareerRepository careerRepository)
    {
        _userRepository = userRepository;
        _firebaseService = firebaseService;
        _careerRepository = careerRepository;
    }
    
    public async Task<ResponseDTO<UserResponseDTO>> CreateUserAsync(UserRequestDTO userRequest)
    {
        try
        {
            var user = new User();
            var fileName = $"{userRequest.name}-{userRequest.dni}";
            var photo = await _firebaseService.UploadProfilePicture(userRequest.Photo, fileName, "image/jpeg");

            user.Name = userRequest.name;
            user.Email = userRequest.email;
            user.Password = userRequest.password;
            user.Dni = userRequest.dni;

            // Carrera
            user.CareerId = userRequest.career;
            user.Career = await _careerRepository.GetByIdAsync(userRequest.career);

            // Foto de perfil
            user.ProfilePic = new Media(fileName, photo);

            await _userRepository.AddAsync(user);

            var userR = new UserResponseDTO
            {
                Name = user.Name,
                Link = user.ProfilePic.Link
            };

            return new ResponseDTO<UserResponseDTO>
            {
                Success = true,
                Message = "Usuario registrado exitosamente",
                Data = userR
            };
        } 
        catch(Exception e)
        {
            var errorMessage = e.InnerException?.Message ?? e.Message;

            if(errorMessage.Contains("Cannot insert duplicate key") && errorMessage.Contains("unique index 'IX_Users_Dni'"))
            {
                errorMessage = "Error: Un usuario con este DNI ha sido registrado anteriormente.";
            }

            if (errorMessage.Contains("The INSERT statement conflicted") && errorMessage.Contains("Careers"))
            {
                errorMessage = "Error: Se está haciendo referencia a una carrera inexistente.";
            }

            return new ResponseDTO<UserResponseDTO>
            {
                Success = false,
                Message = errorMessage,
                Data = null
            };
        }
        
    }
}