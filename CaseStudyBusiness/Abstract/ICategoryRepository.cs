using CaseStudyData.Repository;
using CaseStudyEntity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyBusiness.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Category>> GetSubCategoriesAsync(int categoryId);
        Task AddSubCategoryAsync(Category subCategory);
    }
}
