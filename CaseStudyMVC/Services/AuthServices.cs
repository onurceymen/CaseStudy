using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyMVC.Services
{
    public class AuthService(IHttpClientFactory httpClientFactory) : IAuthService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("api");
        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/auth/register", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanıcı kaydı yapılırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/auth/login", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanıcı girişi yapılırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _httpClient.PostAsync("/api/auth/logout", null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanıcı çıkışı yapılırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/auth/forgotpassword", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Şifre sıfırlama yapılırken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
