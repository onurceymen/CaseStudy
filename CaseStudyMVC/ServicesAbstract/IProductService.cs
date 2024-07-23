using CaseStudyMVC.Models;

namespace CaseStudyMVC.ServicesAbstract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProductsAsync();
        Task<ProductViewModel> GetProductDetailsAsync(int productId);
        Task<bool> CreateProductAsync(CreateProductViewModel model);
        Task<bool> EditProductAsync(EditProductViewModel model);
        Task<bool> DeleteProductAsync(int productId);
        Task<bool> AddCommentAsync(int productId, string comment, int rating);
    }
}
