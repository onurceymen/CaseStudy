using CaseStudyEntity.Entity;

namespace CaseStudyData.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsBySellerIdAsync(int sellerId);
        Task UpdateProductPriceAsync(int productId, decimal newPrice);
        Task UpdateProductStockAsync(int productId, byte newStockAmount);
        Task<Product> GetProductDetailsAsync(int productId);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<Product>> FilterProductsAsync(string category, decimal minPrice, decimal maxPrice);
        Task DeactivateProductAsync(int productId);
        Task AddProductCommentAsync(ProductComment comment);
        Task ApproveProductCommentAsync(int commentId);
        Task<IEnumerable<ProductComment>> FilterProductCommentsAsync(int productId, int starCount, bool? isConfirmed);
    }
}
