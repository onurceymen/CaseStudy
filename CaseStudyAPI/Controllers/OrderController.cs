using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using CaseStudyData.Constants;

namespace CaseStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
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
        [Authorize(Roles = Roles.Buyer)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            try
            {
                var userId = GetUserIdFromToken();
                await _orderService.CreateOrderAsync(orderDto, userId);
                return Ok("Sipariş başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Sipariş oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = Roles.Buyer + "," + Roles.Admin)]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest($"Siparişler getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("details/{orderId}")]
        [Authorize(Roles = Roles.Buyer + "," + Roles.Seller + "," + Roles.Admin)]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderDetailsAsync(orderId);
                if (order == null)
                {
                    return NotFound("Sipariş bulunamadı.");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Sipariş detayları getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = Roles.Buyer + "," + Roles.Admin)]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                await _orderService.CancelOrderAsync(orderId);
                return Ok("Sipariş başarıyla iptal edildi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Sipariş iptal edilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
