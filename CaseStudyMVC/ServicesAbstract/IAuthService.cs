using CaseStudyMVC.Models;
using System.Threading.Tasks;

namespace CaseStudyMVC.ServicesAbstract
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<bool> ForgotPasswordAsync(ForgotPasswordViewModel model);
    }
}
