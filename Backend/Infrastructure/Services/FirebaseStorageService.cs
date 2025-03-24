using Aplication.Interfaces.Helpers;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Services;

public class FirebaseStorageService: IFirebaseStorageService
{
    private readonly string _bucketName;
    
    public FirebaseStorageService(IConfiguration configuration)
    {
        _bucketName = configuration["FIREBASE_BUCKET_NAME"]?? "firebase";
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(configuration["FIREBASE_CREDENTIAL_PATH"]?? "firebase.credentials.json")
            });
        }
    }
    
    public async Task<string> UploadProfilePicture(Stream fileStream, string fileName, string contentType)
    {
        {
            var storage = StorageClient.Create();

            await storage.UploadObjectAsync(_bucketName, fileName, contentType, fileStream);

            return $"https://storage.googleapis.com/{_bucketName}/{fileName}";
        }
    }
}