using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaseStudyMVC.Services
{
    public class HomeService : IHomeService
    {
        private readonly HttpClient _httpClient;

        public HomeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/home/products");
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
                var response = await _httpClient.GetAsync($"/api/home/productdetails/{productId}");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductViewModel>(responseData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ürün detayları getirilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
