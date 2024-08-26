using CaseStudyData.Repository;
using CaseStudyEntity.Entity;

namespace CaseStudyBusiness.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task ActivateUserAsync(int userId);
        Task DeactivateUserAsync(int userId);
        Task ApproveSellerRequestAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task ChangeUserRoleAsync(int userId, int newRoleId);
        Task UpdateAsync(User user);
        Task<bool> CreateUserAsync(User user);

    }
}
