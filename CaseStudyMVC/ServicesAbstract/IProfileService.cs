using CaseStudyMVC.Models;

namespace CaseStudyMVC.ServicesAbstract
{
    public interface IProfileService
    {
        Task<ProfileDetailsViewModel> GetProfileDetailsAsync(string userId);
        Task<bool> EditProfileAsync(EditProfileViewModel model);
        Task<IEnumerable<ProductViewModel>> GetMyProductsAsync(string userId);
        Task<IEnumerable<OrderViewModel>> GetMyOrdersAsync(string userId);
    }
}
