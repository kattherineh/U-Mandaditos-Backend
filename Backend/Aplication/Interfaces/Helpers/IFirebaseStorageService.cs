using Microsoft.AspNetCore.Http;

namespace Aplication.Interfaces.Helpers;

public interface IFirebaseStorageService
{
    Task<string> UploadProfilePicture(IFormFile fileStream, string fileName, string contentType);
}