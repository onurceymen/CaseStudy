using CaseStudyEntity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyData.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsBySellerIdAsync(string sellerId);
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
