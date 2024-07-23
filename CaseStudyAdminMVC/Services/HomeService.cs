using CaseStudyAdminMVC.Models;
using CaseStudyAdminMVC.ServicesAbstract;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaseStudyAdminMVC.Services
{
    public class HomeService : IHomeService
    {
        private readonly HttpClient _httpClient;

        public HomeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<HomeViewModel> GetHomeDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/home");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Anasayfa verilerini getirme işlemi başarısız.");
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<HomeViewModel>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Anasayfa verileri getirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
