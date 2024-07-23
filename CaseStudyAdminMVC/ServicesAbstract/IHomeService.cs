using CaseStudyAdminMVC.Models;

namespace CaseStudyAdminMVC.ServicesAbstract
{
    public interface IHomeService
    {
        Task<HomeViewModel> GetHomeDataAsync();

    }
}
