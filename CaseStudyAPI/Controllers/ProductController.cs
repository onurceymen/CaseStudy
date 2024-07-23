using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            try
            {
                await _productService.AddProductAsync(productDto);
                return Ok("Ürün başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{sellerId}")]
        public async Task<IActionResult> GetProductsBySellerId(string sellerId)
        {
            try
            {
                var products = await _productService.GetProductsBySellerIdAsync(sellerId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Satıcı ürünleri getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("price/{productId}")]
        public async Task<IActionResult> UpdateProductPrice(int productId, decimal newPrice)
        {
            try
            {
                await _productService.UpdateProductPriceAsync(productId, newPrice);
                return Ok("Ürün fiyatı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün fiyatı güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("stock/{productId}")]
        public async Task<IActionResult> UpdateProductStock(int productId, byte newStock)
        {
            try
            {
                await _productService.UpdateProductStockAsync(productId, newStock);
                return Ok("Ürün stoğu başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün stoğu güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("details/{productId}")]
        public async Task<IActionResult> GetProductDetails(int productId)
        {
            try
            {
                var product = await _productService.GetProductDetailsAsync(productId);
                if (product == null)
                {
                    return NotFound("Ürün bulunamadı.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün detayları getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> SearchProducts(string searchTerm)
        {
            try
            {
                var products = await _productService.SearchProductsAsync(searchTerm);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürünler aranırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterProducts(string category, decimal minPrice, decimal maxPrice)
        {
            try
            {
                var products = await _productService.FilterProductsAsync(category, minPrice, maxPrice);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürünler filtrelenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("deactivate/{productId}")]
        public async Task<IActionResult> DeactivateProduct(int productId)
        {
            try
            {
                await _productService.DeactivateProductAsync(productId);
                return Ok("Ürün başarıyla pasif hale getirildi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün pasif hale getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddProductComment(ProductCommentDto commentDto)
        {
            try
            {
                await _productService.AddProductCommentAsync(commentDto);
                return Ok("Ürün yorumu başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün yorumu eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("approve/{commentId}")]
        public async Task<IActionResult> ApproveProductComment(int commentId)
        {
            try
            {
                await _productService.ApproveProductCommentAsync(commentId);
                return Ok("Ürün yorumu başarıyla onaylandı.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün yorumu onaylanırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("comments")]
        public async Task<IActionResult> FilterProductComments(int productId, int starCount, bool? isConfirmed)
        {
            try
            {
                var comments = await _productService.FilterProductCommentsAsync(productId, starCount, isConfirmed);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün yorumları filtrelenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
