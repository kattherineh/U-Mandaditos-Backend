using Aplication.Interfaces.Helpers;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class FirebaseStorageService: IFirebaseStorageService
{
    private readonly string _bucketName;
    private readonly string _credentialsPath;

    public FirebaseStorageService(IConfiguration configuration)
    {
        Console.WriteLine($"FIREBASE_CREDENTIAL_PATH: {configuration["FIREBASE_CREDENTIAL_PATH"]}");

        _bucketName = configuration["Firebase:BucketName"];
        _credentialsPath = configuration["Firebase:CredentialPath"];


        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(_credentialsPath)
            });
        }
    }

    public async Task<string> UploadProfilePicture(IFormFile fileStream, string fileName, string contentType)
    {
        var credentials = GoogleCredential.FromFile(_credentialsPath);
        var storage = StorageClient.Create(credentials);
        var stream = fileStream.OpenReadStream();

        await storage.UploadObjectAsync(_bucketName, fileName, contentType, stream);

        return $"https://storage.googleapis.com/{_bucketName}/{fileName}";
    }
}