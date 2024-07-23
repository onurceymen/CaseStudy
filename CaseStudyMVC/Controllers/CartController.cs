using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseStudyMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = User.FindFirst("sub")?.Value;
                var cartItems = await _cartService.GetCartItemsAsync(userId);
                return View(cartItems);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Sepet öğeleri getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCartItemViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cartService.AddCartItemAsync(model);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Sepet öğesi eklenirken bir hata oluştu.");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Sepet öğesi eklenirken bir hata oluştu: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            try
            {
                var result = await _cartService.RemoveCartItemAsync(cartItemId);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Sepet öğesi silinirken bir hata oluştu.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Sepet öğesi silinirken bir hata oluştu: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            try
            {
                var result = await _cartService.UpdateCartItemQuantityAsync(cartItemId, quantity);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Sepet öğesi güncellenirken bir hata oluştu.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Sepet öğesi güncellenirken bir hata oluştu: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
