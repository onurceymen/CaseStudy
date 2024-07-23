using CaseStudyAdminMVC.Models;

namespace CaseStudyAdminMVC.ServicesAbstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<bool> ApproveSellerAsync(int userId);
    }
}
