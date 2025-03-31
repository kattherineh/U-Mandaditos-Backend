using Aplication.DTOs;
using Aplication.DTOs.General;

namespace Aplication.Interfaces;

public interface IManagementService {
    Task<ResponseDTO<ManagementPwdResponseDTO?>> SendEmailAsync(string email);

    Task<ResponseDTO<bool>> CompareCodeAsync(int idManagement, string code);

    Task<bool> DeactivateManagementAsync(int idManagement);
}