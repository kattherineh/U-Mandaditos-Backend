namespace Aplication.Interfaces.Helpers;

public interface IFirebaseStorageService
{
    Task<string> UploadProfilePicture(Stream fileStream, string fileName, string contentType);
}