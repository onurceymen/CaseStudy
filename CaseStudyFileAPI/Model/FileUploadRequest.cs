using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyFileAPI.Model
{
    public class FileUploadRequest
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string ContainerName { get; set; }
    }
}
