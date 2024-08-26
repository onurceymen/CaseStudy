using CaseStudyBusiness.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyAPI.ServicesAbstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByEmailAsync(string email);
        Task ActivateUserAsync(int userId);
        Task DeactivateUserAsync(int userId);
        Task ApproveSellerRequestAsync(int userId);
        Task RegisterUserAsync(UserCreateDto userCreateDto);
        Task<UserLoginResponseDto> AuthenticateUserAsync(UserLoginDto userLoginDto);
        Task UpdateUserByEmailAsync(string email, UserUpdateDto updatedUser);
        Task ChangeUserRoleAsync(int userId, int newRoleId);





    }
}
