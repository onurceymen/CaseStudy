using CaseStudyBusiness.Dtos;

namespace CaseStudyAPI.ServicesAbstract
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDto>> GetCartItemsByUserIdAsync(string userId);
        Task<bool> AddCartItemAsync(CartItemDto cartItem);
        Task RemoveCartItemAsync(int cartItemId);
        Task UpdateCartItemQuantityAsync(int cartItemId, byte newQuantity);
        Task<CartItemDto> GetCartItemByUserAndProductIdAsync(string userId, int productId);
    }
}
