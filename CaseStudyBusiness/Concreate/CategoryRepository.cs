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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly CaseStudyDbContext _context;

        public CategoryRepository(CaseStudyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int categoryId)
        {
            return await _context.Categories
                .Where(c => c.ParentCategoryId == categoryId)
                .ToListAsync();
        }

        public async Task AddSubCategoryAsync(Category subCategory)
        {
            await _context.Categories.AddAsync(subCategory);
            await _context.SaveChangesAsync();
        }
    }
}
