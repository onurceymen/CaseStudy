using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CaseStudyData.Context;
using CaseStudyEntity.Entity;

namespace CaseStudyData.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly CaseStudyDbContext _context;

        public ProductRepository(CaseStudyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsBySellerIdAsync(string sellerId)
        {
            return await _context.Products
                .Where(p => p.SellerId == sellerId)
                .ToListAsync();
        }

        public async Task UpdateProductPriceAsync(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Price = newPrice;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProductStockAsync(int productId, byte newStockAmount)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.StockAmount = newStockAmount;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductDetailsAsync(int productId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductComments)
                .SingleOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Details.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterProductsAsync(string category, decimal minPrice, decimal maxPrice)
        {
            return await _context.Products
                .Where(p => p.Category.Name == category && p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();
        }

        public async Task DeactivateProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Enabled = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddProductCommentAsync(ProductComment comment)
        {
            await _context.ProductComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveProductCommentAsync(int commentId)
        {
            var comment = await _context.ProductComments.FindAsync(commentId);
            if (comment != null)
            {
                comment.IsConfirmed = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductComment>> FilterProductCommentsAsync(int productId, int starCount, bool? isConfirmed)
        {
            var query = _context.ProductComments.AsQueryable();

            if (productId > 0)
            {
                query = query.Where(pc => pc.ProductId == productId);
            }

            if (starCount > 0)
            {
                query = query.Where(pc => pc.StarCount == starCount);
            }

            if (isConfirmed.HasValue)
            {
                query = query.Where(pc => pc.IsConfirmed == isConfirmed.Value);
            }

            return await query.ToListAsync();
        }
    }
}
