using Aplication.Interfaces;
using Aplication.Interfaces.Helpers;
using Infrastructure.Persistence;
using Domain.Entities;
using Aplication.Interfaces.Users;

namespace Infrastructure.Repositories;

public class ManagementRepository : IManagementRepository
{
    private readonly BackendDbContext _context;
    private readonly IEmailService _emailService;
    private readonly ICodeGeneratorService _codeGeneratorService;
    private readonly IUserService _userService;

    public ManagementRepository(BackendDbContext context, IEmailService emailService, ICodeGeneratorService codeGeneratorService, IUserService userService)
    {
        _context = context;
        _emailService = emailService;
        _codeGeneratorService = codeGeneratorService;
        _userService = userService;
    }

    public async Task<Management> AddAsync(Management management)
    {
        var mng = await _context.Managements.AddAsync(management);
        await _context.SaveChangesAsync();
        return mng.Entity;
    }

    public async Task<string> GetCodeByIdAsync(int managementId)
    {
        var management = await _context.Managements.FindAsync(managementId);
        return management?.Code ?? string.Empty;
    }

    public async Task<bool> DeactivateAsync(int managementId)
    {
        var management = await _context.Managements.FindAsync(managementId);
        if (management != null)
        {
            management.Active = false;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> CompareCodeAsync(int managementId, string code)
    {
        var management = await _context.Managements.FindAsync(managementId);
        if (management == null || management.Code != code)
        {
            return false;
        }

        management.Active = false;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeactivateManagementAsync(int managementId)
    {
        // Implementation for deactivating management
        var management = await _context.Managements.FindAsync(managementId);
        if (management != null)
        {
            management.Active = false;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

}
