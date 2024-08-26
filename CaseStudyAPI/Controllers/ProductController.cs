using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Dtos;
using CaseStudyData.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CaseStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        private int GetSellerIdFromToken()
        {
            return int.Parse(User.FindFirst("SellerId")?.Value);
        }

        private int GetUserIdFromToken()
        {
            return int.Parse(User.FindFirst("UserId")?.Value);
        }

        private string GetUserRoleFromToken()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Seller)]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto productDto)
        {
            try
            {
                var sellerId = GetSellerIdFromToken();
                await _productService.AddProductAsync(productDto, sellerId);
                return Ok("Ürün başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{sellerId:int}")]
        [Authorize(Roles = Roles.Seller)]
        public async Task<IActionResult> GetProductsBySellerId(int sellerId)
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

        [HttpPut("price/{productId:int}")]
        [Authorize(Roles = Roles.Seller)]
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

        [HttpPut("stock/{productId:int}")]
        [Authorize(Roles = Roles.Seller)]
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

        [HttpGet("details/{productId:int}")]
        [Authorize(Roles = Roles.Buyer + "," + Roles.Seller)]
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
        [Authorize(Roles = Roles.Buyer + "," + Roles.Seller)]
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
        [Authorize(Roles = Roles.Buyer + "," + Roles.Seller)]
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

        [HttpPut("deactivate/{productId:int}")]
        [Authorize(Roles = Roles.Seller)]
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
        [Authorize(Roles = Roles.Buyer)]
        public async Task<IActionResult> AddProductComment([FromBody] CreateProductCommentDto commentDto)
        {
            try
            {
                var userId = GetUserIdFromToken();
                await _productService.AddProductCommentAsync(commentDto, userId);
                return Ok("Ürün yorumu başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ürün yorumu eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("approve/{commentId:int}")]
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin + "," + Roles.Seller)]
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
