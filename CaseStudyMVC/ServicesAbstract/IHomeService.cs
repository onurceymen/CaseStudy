using CaseStudyMVC.Models;

namespace CaseStudyMVC.ServicesAbstract
{
    public interface IHomeService
    {
        Task<IEnumerable<ProductViewModel>> GetProductsAsync();
        Task<ProductViewModel> GetProductDetailsAsync(int productId);
    }
}
