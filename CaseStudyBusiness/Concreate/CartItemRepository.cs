using CaseStudyBusiness.Abstract;
using CaseStudyData.Context;
using CaseStudyData.Repository;
using CaseStudyEntity.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBusiness.Concreate
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly CaseStudyDbContext _context;

        public CartItemRepository(CaseStudyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId)
        {
            return await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Product)
                .ToListAsync();
        }

        public async Task<CartItem> GetCartItemByUserAndProductIdAsync(string userId, int productId)
        {
            return await _context.CartItems
                .SingleOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);
        }

        public async Task UpdateCartItemQuantityAsync(int cartItemId, byte newQuantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = newQuantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
