using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CaseStudyFileAPI.Model;

namespace CaseStudyFileAPI.Services
{
    public class AzureBlobService : IFilesService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobService(IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureBlobConnectionString");
            _containerName = Environment.GetEnvironmentVariable("AzureBlobContainerName");
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<FileUploadResult> UploadFileAsync(IFormFile file, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            return new FileUploadResult
            {
                FileName = blobClient.Name,
                Url = blobClient.Uri.ToString()
            };
        }

        public async Task<Stream> DownloadFileAsync(string fileName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var downloadInfo = await blobClient.DownloadAsync();

            return downloadInfo.Value.Content;
        }

        public async Task DeleteFileAsync(string fileName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
