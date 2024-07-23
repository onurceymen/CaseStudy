using CaseStudyBusiness.Dtos;

namespace CaseStudyAPI.ServicesAbstract
{
    public interface IProductService
    {
        Task AddProductAsync(ProductDto product);
        Task<IEnumerable<ProductDto>> GetProductsBySellerIdAsync(string sellerId);
        Task UpdateProductPriceAsync(int productId, decimal newPrice);
        Task UpdateProductStockAsync(int productId, byte newStock);
        Task<ProductDto> GetProductDetailsAsync(int productId);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<ProductDto>> FilterProductsAsync(string category, decimal minPrice, decimal maxPrice);
        Task DeactivateProductAsync(int productId);
        Task AddProductCommentAsync(ProductCommentDto comment);
        Task ApproveProductCommentAsync(int commentId);
        Task<IEnumerable<ProductCommentDto>> FilterProductCommentsAsync(int productId, int starCount, bool? isConfirmed);
    }
}
