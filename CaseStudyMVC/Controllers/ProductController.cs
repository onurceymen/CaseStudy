using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseStudyMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                return View(products);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ürünler getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int productId)
        {
            try
            {
                var product = await _productService.GetProductDetailsAsync(productId);
                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ürün detayları getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.CreateProductAsync(model);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Ürün oluşturulurken bir hata oluştu.");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ürün oluşturulurken bir hata oluştu: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            try
            {
                var product = await _productService.GetProductDetailsAsync(productId);
                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ($"Ürün düzenleme sayfası getirilirken bir hata oluştu: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.EditProductAsync(model);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Ürün düzenlenirken bir hata oluştu.");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ürün düzenlenirken bir hata oluştu: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(productId);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Ürün silinirken bir hata oluştu.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ürün silinirken bir hata oluştu: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
