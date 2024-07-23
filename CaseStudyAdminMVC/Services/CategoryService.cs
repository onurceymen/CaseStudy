using CaseStudyAdminMVC.Models;
using CaseStudyAdminMVC.ServicesAbstract;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyAdminMVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<bool> CreateCategoryAsync(CategoryViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/category", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Kategori oluşturma işlemi başarısız.");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Kategori oluşturulurken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<bool> EditCategoryAsync(int id, CategoryViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/api/category/{id}", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Kategori düzenleme işlemi başarısız.");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Kategori düzenlenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/category/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Kategori silme işlemi başarısız.");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Kategori silinirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<CategoryViewModel> GetCategoryAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/category/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Kategori getirme işlemi başarısız.");
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CategoryViewModel>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Kategori getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/category");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Kategorileri getirme işlemi başarısız.");
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Kategoriler getirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
