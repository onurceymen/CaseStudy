using CaseStudyBusiness.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyAPI.ServicesAbstract
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDto>> GetCartItemsByUserIdAsync(int userId);
        Task<bool> AddCartItemAsync(CartItemDto cartItemDto);
        Task RemoveCartItemAsync(int cartItemId);
        Task UpdateCartItemQuantityAsync(int cartItemId, byte newQuantity);
        Task<CartItemDto> GetCartItemByUserAndProductIdAsync(int userId, int productId);
    }
}
