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
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;

        public ProfileService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<ProfileDetailsViewModel> GetProfileDetailsAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/profile/{userId}");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProfileDetailsViewModel>(responseData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Profil bilgileri getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<bool> EditProfileAsync(EditProfileViewModel model)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync("/api/profile/edit", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Profil düzenlenirken bir hata oluştu: {response.ReasonPhrase}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Profil düzenlenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetMyProductsAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/profile/myproducts/{userId}");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(responseData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanıcının ürünleri getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IEnumerable<OrderViewModel>> GetMyOrdersAsync(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/profile/myorders/{userId}");
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<OrderViewModel>>(responseData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Kullanıcının siparişleri getirilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
