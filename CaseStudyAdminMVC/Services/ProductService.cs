using CaseStudyAdminMVC.ServicesAbstract;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaseStudyAdminMVC.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/product/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Ürün silme işlemi başarısız.");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Ürün silinirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
