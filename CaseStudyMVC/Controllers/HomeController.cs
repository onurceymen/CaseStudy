using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseStudyMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _homeService.GetProductsAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ürünler getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> ProductDetail(int productId)
        {
            try
            {
                var product = await _homeService.GetProductDetailsAsync(productId);
                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ürün detayları getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }
    }
}
