using CaseStudyFileAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyFileAPI.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;

        public FilesController(IFilesService filesService)
        {
            _filesService = filesService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya yüklenemedi.");

            var fileUrl = await _filesService.UploadFileAsync(file, "your-container-name");
            return Ok(new { FileUrl = fileUrl.Url });
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile(string fileName, string containerName)
        {
            var fileStream = await _filesService.DownloadFileAsync(fileName, containerName);
            return File(fileStream, "application/octet-stream");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFile(string fileName, string containerName)
        {
            await _filesService.DeleteFileAsync(fileName, containerName);
            return NoContent();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateFile(string fileName, string containerName, IFormFile newFile)
        {
            if (newFile == null || newFile.Length == 0)
                return BadRequest("Dosya yüklenemedi.");

            // Eski dosyayı sil
            await _filesService.DeleteFileAsync(fileName, containerName);

            // Yeni dosyayı yükle
            var fileUploadResult = await _filesService.UploadFileAsync(newFile, containerName);
            return Ok(new { FileUrl = fileUploadResult.Url });
        }
    }
}
