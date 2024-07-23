using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseStudyMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = User.FindFirst("sub")?.Value;
                var orders = await _orderService.GetOrdersAsync(userId);
                return View(orders);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Siparişler getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderDetailsAsync(orderId);
                return View(order);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Sipariş detayları getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _orderService.CreateOrderAsync(model);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Sipariş oluşturulurken bir hata oluştu.");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Sipariş oluşturulurken bir hata oluştu: {ex.Message}");
                return View(model);
            }
        }
    }
}
