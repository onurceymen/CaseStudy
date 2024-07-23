using CaseStudyData.Repository;
using CaseStudyEntity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyBusiness.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task ActivateUserAsync(string userId);
        Task DeactivateUserAsync(string userId);
        Task ApproveSellerRequestAsync(string userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task ChangeUserRoleAsync(string userId, string newRoleId);

    }
}
