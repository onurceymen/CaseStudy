using CaseStudyBusiness.Abstract;
using CaseStudyBusiness.Dtos;

namespace CaseStudyAPI.ServicesAbstract
{
    public interface ICategoryService 
    {
        Task AddCategoryAsync(CategoryDto category);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<CategoryDto>> GetSubCategoriesAsync(int categoryId);
        Task AddSubCategoryAsync(CategoryDto subCategory);
    }
}
