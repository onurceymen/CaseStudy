using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyMVC.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<IEnumerable<CartItemViewModel>> GetCartItemsAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/cart/user/{userId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CartItemViewModel>>(content);
            }
            catch (Exception ex)
            {
                throw new Exception($"Sepet öğeleri getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> AddCartItemAsync(AddCartItemViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/cart", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Sepet öğesi eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> RemoveCartItemAsync(int cartItemId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/cart/{cartItemId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Sepet öğesi kaldırılırken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> UpdateCartItemQuantityAsync(int cartItemId, int quantity)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { Quantity = quantity }), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/api/cart/{cartItemId}/quantity", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Sepet öğesi güncellenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
