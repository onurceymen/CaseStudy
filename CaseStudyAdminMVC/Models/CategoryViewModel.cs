using System.ComponentModel.DataAnnotations;

namespace CaseStudyAdminMVC.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string IconCssClass { get; set; }
    }
}
