using CaseStudyData.Repository;
using CaseStudyEntity.Entity;

namespace CaseStudyBusiness.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Category>> GetSubCategoriesAsync(int categoryId);
        Task AddSubCategoryAsync(Category subCategory);
    }
}
