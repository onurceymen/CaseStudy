using CaseStudyAdminMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CaseStudyAdminMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var comments = await _commentService.GetCommentsAsync();
                return View(comments);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Yorumları getirirken bir hata oluştu: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var result = await _commentService.ApproveCommentAsync(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Yorum onaylama işlemi başarısız.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Yorum onaylanırken bir hata oluştu: " + ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
