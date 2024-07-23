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
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/order/{userId}");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<OrderViewModel>>(responseData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Siparişler getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<OrderViewModel> GetOrderDetailsAsync(int orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/order/details/{orderId}");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderViewModel>(responseData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Sipariş detayları getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> CreateOrderAsync(CreateOrderViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/order/create", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Sipariş oluşturulurken bir hata oluştu: {response.ReasonPhrase}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Sipariş oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
