using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
namespace ClimbingApplication.Service
{
    public class FirebaseImageService : IImageService
    {
        private readonly string _bucket;
        private readonly string _credentialPath;

        public FirebaseImageService(IConfiguration configuration)
        {
            _bucket = configuration["Firebase:StorageBucket"];
            _credentialPath = configuration["Firebase:ServiceAccountPath"];

            if (string.IsNullOrEmpty(_credentialPath) || string.IsNullOrEmpty(_bucket))
            {
                throw new InvalidOperationException("Hiányzik a Firebase Storage konfiguráció (Bucket vagy Credential Path).");

            }

        }

        public string ExtractFileNameFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var uri = new Uri(url);
            var path = uri.LocalPath;

            var fileName = Uri.UnescapeDataString(path.Split('/').Last());

            return fileName;
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            var fileName = ExtractFileNameFromUrl(imageUrl);

            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
        
            var credential = GoogleCredential.FromFile(_credentialPath);
            var storage = StorageClient.Create(credential);

            await storage.DeleteObjectAsync(_bucket, fileName);
        }
    }
}
