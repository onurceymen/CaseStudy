using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyMVC.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/product");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(responseData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ürünler getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ProductViewModel> GetProductDetailsAsync(int productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/product/{productId}");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductViewModel>(responseData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ürün detayları getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> CreateProductAsync(CreateProductViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/product/create", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ürün oluşturulurken bir hata oluştu: {response.ReasonPhrase}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ürün oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> EditProductAsync(EditProductViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync("/api/product/edit", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ürün düzenlenirken bir hata oluştu: {response.ReasonPhrase}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ürün düzenlenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/product/delete/{productId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ürün silinirken bir hata oluştu: {response.ReasonPhrase}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ürün silinirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> AddCommentAsync(int productId, string comment, int rating)
        {
            try
            {
                var model = new { productId, comment, rating };
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/product/comment", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Yorum eklenirken bir hata oluştu: {response.ReasonPhrase}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Yorum eklenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
