using CaseStudyAdminMVC.Models;
using CaseStudyAdminMVC.ServicesAbstract;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaseStudyAdminMVC.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/user");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Kullanıcıları getirme işlemi başarısız.");
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Kullanıcılar getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<bool> ApproveSellerAsync(int userId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"/api/user/approve/{userId}", null);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Satıcı onaylama işlemi başarısız.");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Satıcı onaylanırken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
