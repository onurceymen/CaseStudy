using CaseStudyMVC.Models;

namespace CaseStudyMVC.ServicesAbstract
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemViewModel>> GetCartItemsAsync(string userId);
        Task<bool> AddCartItemAsync(AddCartItemViewModel model);
        Task<bool> RemoveCartItemAsync(int cartItemId);
        Task<bool> UpdateCartItemQuantityAsync(int cartItemId, int quantity);
    }
}
