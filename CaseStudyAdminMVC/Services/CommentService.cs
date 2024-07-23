using CaseStudyAdminMVC.Models;
using CaseStudyAdminMVC.ServicesAbstract;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaseStudyAdminMVC.Services
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        public CommentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task<IEnumerable<CommentViewModel>> GetCommentsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/comment");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Yorumları getirme işlemi başarısız.");
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<CommentViewModel>>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Yorumlar getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<bool> ApproveCommentAsync(int commentId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"/api/comment/approve/{commentId}", null);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Yorum onaylama işlemi başarısız.");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Yorum onaylanırken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
