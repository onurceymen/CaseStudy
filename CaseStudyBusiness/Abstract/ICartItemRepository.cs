using CaseStudyData.Repository;
using CaseStudyEntity.Entity;

namespace CaseStudyBusiness.Abstract
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId);
        Task<CartItem> GetCartItemByUserAndProductIdAsync(int userId, int productId);
        Task UpdateCartItemQuantityAsync(int cartItemId, byte newQuantity);
    }
}
