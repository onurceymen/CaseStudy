using CaseStudyBusiness.Dtos;

namespace CaseStudyAPI.ServicesAbstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByEmailAsync(string email);
        Task ActivateUserAsync(string userId);
        Task DeactivateUserAsync(string userId);
        Task ApproveSellerRequestAsync(string userId);
        Task RegisterUserAsync(UserDto user, string password);
        Task<UserDto> AuthenticateUserAsync(string email, string password);
        Task UpdateUserByEmailAsync(string email, UserDto updatedUser);
        Task ChangeUserRoleAsync(string userId, string newRoleId);
    }
}
