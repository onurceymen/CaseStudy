using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartItemsByUserId(string userId)
        {
            try
            {
                var cartItems = await _cartService.GetCartItemsByUserIdAsync(userId);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return BadRequest($"Sepet öğeleri getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem(CartItemDto cartItemDto)
        {
            try
            {
                var result = await _cartService.AddCartItemAsync(cartItemDto);
                if (result)
                {
                    return Ok("Sepet öğesi başarıyla eklendi.");
                }
                return BadRequest("Sepet öğesi eklenemedi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Sepet öğesi eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            try
            {
                await _cartService.RemoveCartItemAsync(cartItemId);
                return Ok("Sepet öğesi başarıyla kaldırıldı.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Sepet öğesi kaldırılırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int cartItemId, byte newQuantity)
        {
            try
            {
                await _cartService.UpdateCartItemQuantityAsync(cartItemId, newQuantity);
                return Ok("Sepet öğesi miktarı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Sepet öğesi miktarı güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{userId}/{productId}")]
        public async Task<IActionResult> GetCartItemByUserAndProductId(string userId, int productId)
        {
            try
            {
                var cartItem = await _cartService.GetCartItemByUserAndProductIdAsync(userId, productId);
                if (cartItem == null)
                {
                    return NotFound("Kullanıcı ve ürün için sepet öğesi bulunamadı.");
                }
                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return BadRequest($"Sepet öğesi getirilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
