using Domain.Entities;

namespace Aplication.Interfaces
{
    public interface IManagementRepository
    {
        Task<Management> AddAsync(Management management);
        Task<bool> CompareCodeAsync(int idManagement, string code);

        Task<bool> DeactivateManagementAsync(int idManagement);

        Task<string> GetCodeByIdAsync(int idManagement);
        Task<bool> DeactivateAsync(int idManagement);
    }
}