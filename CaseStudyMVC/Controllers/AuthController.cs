using CaseStudyMVC.Models;
using CaseStudyMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseStudyMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authService.RegisterAsync(model);
                    if (result)
                    {
                        return RedirectToAction("Login");
                    }
                    ModelState.AddModelError(string.Empty, "Kayıt işlemi başarısız oldu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Kayıt sırasında bir hata oluştu: {ex.Message}");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authService.LoginAsync(model);
                    if (result)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Giriş işlemi başarısız oldu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Giriş sırasında bir hata oluştu: {ex.Message}");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.LogoutAsync();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Çıkış sırasında bir hata oluştu: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
