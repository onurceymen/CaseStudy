using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            try
            {
                await _orderService.CreateOrderAsync(orderDto);
                return Ok("Sipariş başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Sipariş oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
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
