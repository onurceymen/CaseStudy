using CaseStudyFileAPI.Model;

namespace CaseStudyFileAPI.Services
{
    public interface IFilesService
    {
        Task<FileUploadResult> UploadFileAsync(IFormFile file, string containerName);
        Task<Stream> DownloadFileAsync(string fileName, string containerName);
        Task DeleteFileAsync(string fileName, string containerName);
    }
}
