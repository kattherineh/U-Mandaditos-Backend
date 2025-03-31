using Aplication.DTOs;
using Aplication.Interfaces;
using Aplication.Interfaces.Helpers;
using Aplication.Interfaces.Users;
using Domain.Entities;
using Aplication.DTOs.General;

namespace Aplication.Services;

public class ManagementService : IManagementService
{
    private readonly IEmailService _emailService;
    private readonly IManagementRepository _managementRepository;
    private readonly ICodeGeneratorService _codeGeneratorService;
    private readonly IUserService _userService;

    public ManagementService(IEmailService emailService, IManagementRepository managementRepository, IUserService userService, ICodeGeneratorService codeGeneratorService)
    {
        _emailService = emailService;
        _managementRepository = managementRepository;
        _userService = userService;
        _codeGeneratorService = codeGeneratorService;
    }

    public async Task<ResponseDTO<ManagementPwdResponseDTO?>> SendEmailAsync(string email)
    {
        try
        {
            // Buscar usuario
            var user = await _userService.GetByEmailAsync(email);
            Console.WriteLine(user?.Id + "usuarioooooooooooo");
            if (user == null)
            {
                return new ResponseDTO<ManagementPwdResponseDTO?>
                {
                    Success = false,
                    Message = "Usuario no encontrado",
                    Data = null
                };
            }

            // Generar código de verificación
            var code = _codeGeneratorService.GenerateVerifyEmailCode();
            if (string.IsNullOrEmpty(code))
            {
                return new ResponseDTO<ManagementPwdResponseDTO?>
                {
                    Success = false,
                    Message = "Error al generar el código de verificación",
                    Data = null
                };
            }

            Console.WriteLine(code);
            Console.WriteLine(user + " 1");

            var management = new Management
            {
                UserId = user.Id,
                ManegementRoleId = 18,
                Code = code,
                Active = true
            };

            Console.WriteLine(management.ToString() + " 2");

            // Enviar correo
            bool success = await _emailService.SendEmailCodeAsync(email, code);
            if (!success)
            {
                return new ResponseDTO<ManagementPwdResponseDTO?>
                {
                    Success = false,
                    Message = "Error al enviar el correo",
                    Data = null
                };
            }

            Console.WriteLine(management.Active);
            Console.WriteLine(management.Code);
            Console.WriteLine(management.UserId + "user");
            // Guardar en la base de datos
 
            var mng = await _managementRepository.AddAsync(management);
            Console.WriteLine(mng + " 2");
 
            return new ResponseDTO<ManagementPwdResponseDTO?>
            {
                Success = true,
                Message = "Código enviado correctamente",
                Data = new ManagementPwdResponseDTO
                {
                    IdManagement = mng.Id,
                }
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ManagementPwdResponseDTO?>
            {
                Success = false,
                Message = "Ocurrió un error inesperado: " + ex.Message,
                Data = null
            };
        }
    }

    public async Task<ResponseDTO<bool>> CompareCodeAsync(int id, string code)
    {
        // Implementation for comparing code
        var storedCode = await _managementRepository.GetCodeByIdAsync(id);
        if(storedCode == code)
        {
            await DeactivateManagementAsync(id);
            return new ResponseDTO<bool>
            {
                Success = true,
                Message = "Comaración de código exitosa",
                Data = true
            };
        }

        return new ResponseDTO<bool>
        {
            Success = false,
            Message = "El código no coincide",
            Data = false
        };
    }

    public async Task<bool> DeactivateManagementAsync(int id)
    {
        // Implementation for deactivating management
        await _managementRepository.DeactivateAsync(id);
        return true; // Return true to match the expected return type
    }
}