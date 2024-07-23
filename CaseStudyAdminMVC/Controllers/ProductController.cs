using CaseStudyAdminMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CaseStudyAdminMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Ürün silme işlemi başarısız.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ürün silinirken bir hata oluştu: " + ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
