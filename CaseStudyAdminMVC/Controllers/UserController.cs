using CaseStudyAdminMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CaseStudyAdminMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return View(users);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kullanıcıları getirirken bir hata oluştu: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var result = await _userService.ApproveSellerAsync(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Satıcı onaylama işlemi başarısız.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Satıcı onaylanırken bir hata oluştu: " + ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
