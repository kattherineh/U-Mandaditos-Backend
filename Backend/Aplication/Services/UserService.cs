using Aplication.DTOs;
using Aplication.DTOs.General;
using Aplication.DTOs.Locations;
using Aplication.DTOs.Media;
using Aplication.DTOs.Users;
using Aplication.Interfaces;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Users;
using Domain.Entities;

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

    public async Task<ResponseDTO<UserProfileResponseDTO>> GetByIdAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return new ResponseDTO<UserProfileResponseDTO>
                {
                    Success = false,
                    Message = $"Usuario con id={id} no encontrado"
                };
            }

            var data = new UserProfileResponseDTO
            {
                Name = user.Name,
                Dni = user.Dni,
                Email = user.Email,
                BirthDay = user.BirthDay,
                Score = user.Rating,
                ProfilePic = new MediaResponseDTO
                {
                    Id = user.ProfilePic.Id,
                    Name = user.ProfilePic.Name,
                    Link = user.ProfilePic.Link
                },
                LastLocation = new LastLocationUserDTO
                {
                    Description = user.LastLocation.Description,
                    Name = user.LastLocation.Name,
                    Id = user.LastLocation.Id
                },
                Career = new CareerResponseDTO
                {
                    Id = user.Career.Id,
                    Name = user.Career.Name
                }
            };

            return new ResponseDTO<UserProfileResponseDTO>
            {
                Success = true,
                Message = $"La información del usuario con id={id} fue obtenida satisfactoriamente",
                Data = data
            };
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return new ResponseDTO<UserProfileResponseDTO>
            {
                Success = false,
                Message = $"Ocurrió un error al obtener al usuario con id={id}"
            };
        }


        return null;
    }
}