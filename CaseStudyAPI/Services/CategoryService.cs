using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Abstract;
using CaseStudyBusiness.Dtos;
using CaseStudyEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategoryAsync(CategoryDto categoryDto)
        {
            try
            {
                var category = new Category
                {
                    Name = categoryDto.Name,
                    Color = categoryDto.Color,
                    IconCssClass = categoryDto.IconCssClass,
                    CreatedAt = DateTime.Now
                };

                await _categoryRepository.AddAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception("Kategori eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId)
        {
            try
            {
                var products = await _categoryRepository.GetProductsByCategoryIdAsync(categoryId);
                return products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Details = p.Details,
                    StockAmount = p.StockAmount,
                    CategoryId = p.CategoryId,
                    CreatedAt = p.CreatedAt,
                    Enabled = p.Enabled
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Kategori ürünleri getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetSubCategoriesAsync(int categoryId)
        {
            try
            {
                var subCategories = await _categoryRepository.GetSubCategoriesAsync(categoryId);
                return subCategories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Color = c.Color,
                    IconCssClass = c.IconCssClass,
                    CreatedAt = c.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Alt kategoriler getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task AddSubCategoryAsync(CategoryDto subCategoryDto)
        {
            try
            {
                var subCategory = new Category
                {
                    Name = subCategoryDto.Name,
                    Color = subCategoryDto.Color,
                    IconCssClass = subCategoryDto.IconCssClass,
                    CreatedAt = DateTime.Now,
                    ParentCategoryId = subCategoryDto.Id
                };

                await _categoryRepository.AddSubCategoryAsync(subCategory);
            }
            catch (Exception ex)
            {
                throw new Exception("Alt kategori eklenirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
