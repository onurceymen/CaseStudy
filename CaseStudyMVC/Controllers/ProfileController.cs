using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseStudyMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = User.FindFirst("sub")?.Value;
                var profileDetails = await _profileService.GetProfileDetailsAsync(userId);
                return View(profileDetails);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Profil bilgileri getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            try
            {
                var userId = User.FindFirst("sub")?.Value;
                var profileDetails = await _profileService.GetProfileDetailsAsync(userId);
                return View(profileDetails);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Profil düzenleme sayfası getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _profileService.EditProfileAsync(model);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Profil düzenlenirken bir hata oluştu.");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Profil düzenlenirken bir hata oluştu: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> MyOrders()
        {
            try
            {
                var userId = User.FindFirst("sub")?.Value;
                var orders = await _profileService.GetMyOrdersAsync(userId);
                return View(orders);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Siparişler getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }

        public async Task<IActionResult> MyProducts()
        {
            try
            {
                var userId = User.FindFirst("sub")?.Value;
                var products = await _profileService.GetMyProductsAsync(userId);
                return View(products);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ürünler getirilirken bir hata oluştu: {ex.Message}";
                return View("Error");
            }
        }
    }
}
