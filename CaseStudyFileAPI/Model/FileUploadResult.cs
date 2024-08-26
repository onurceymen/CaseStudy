using System.ComponentModel.DataAnnotations;

namespace CaseStudyFileAPI.Model
{
    public class FileUploadResult
    {
        [Key]
        public string FileName { get; set; }
        public string Url { get; set; }
    }
}
