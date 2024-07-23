using CaseStudyAdminMVC.Models;

namespace CaseStudyAdminMVC.ServicesAbstract
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryAsync(CategoryViewModel model);
        Task<bool> EditCategoryAsync(int id, CategoryViewModel model);
        Task<bool> DeleteCategoryAsync(int id);
        Task<CategoryViewModel> GetCategoryAsync(int id);
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
    }
}
