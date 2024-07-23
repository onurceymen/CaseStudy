using CaseStudyAdminMVC.Models;
using CaseStudyAdminMVC.ServicesAbstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CaseStudyAdminMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kategorileri getirirken bir hata oluştu: " + ex.Message);
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _categoryService.CreateCategoryAsync(model);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Kategori oluşturma işlemi başarısız.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kategori oluşturulurken bir hata oluştu: " + ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryAsync(id);
                return View(category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kategori getirilirken bir hata oluştu: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _categoryService.EditCategoryAsync(id, model);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Kategori düzenleme işlemi başarısız.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kategori düzenlenirken bir hata oluştu: " + ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Kategori silme işlemi başarısız.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kategori silinirken bir hata oluştu: " + ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
