using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Dtos;
using CaseStudyData.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CaseStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                await _categoryService.AddCategoryAsync(categoryDto);
                return Ok("Kategori başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Kategori eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var products = await _categoryService.GetProductsByCategoryIdAsync(categoryId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Kategori ürünleri getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("subcategories/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubCategories(int categoryId)
        {
            try
            {
                var subCategories = await _categoryService.GetSubCategoriesAsync(categoryId);
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Alt kategoriler getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("subcategories")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AddSubCategory([FromBody] CategoryDto subCategoryDto)
        {
            try
            {
                await _categoryService.AddSubCategoryAsync(subCategoryDto);
                return Ok("Alt kategori başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Alt kategori eklenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
