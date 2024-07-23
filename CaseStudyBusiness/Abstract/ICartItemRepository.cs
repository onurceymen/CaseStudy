using CaseStudyData.Repository;
using CaseStudyEntity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyBusiness.Abstract
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId);
        Task<CartItem> GetCartItemByUserAndProductIdAsync(string userId, int productId);
        Task UpdateCartItemQuantityAsync(int cartItemId, byte newQuantity);
    }
}
