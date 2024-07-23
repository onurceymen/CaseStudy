using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Abstract;
using CaseStudyBusiness.Dtos;
using CaseStudyData.Repository;
using CaseStudyEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            try
            {
                var product = new Product
                {
                    SellerId = productDto.SellerId,
                    CategoryId = productDto.CategoryId,
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Details = productDto.Details,
                    StockAmount = productDto.StockAmount,
                    CreatedAt = DateTime.Now,
                    Enabled = productDto.Enabled
                };

                await _productRepository.AddAsync(product);
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProductsBySellerIdAsync(string sellerId)
        {
            try
            {
                var products = await _productRepository.GetProductsBySellerIdAsync(sellerId);
                return products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Details = p.Details,
                    StockAmount = p.StockAmount,
                    CategoryId = p.CategoryId,
                    SellerId = p.SellerId,
                    CreatedAt = p.CreatedAt,
                    Enabled = p.Enabled
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Satıcı ürünleri getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task UpdateProductPriceAsync(int productId, decimal newPrice)
        {
            try
            {
                await _productRepository.UpdateProductPriceAsync(productId, newPrice);
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün fiyatı güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task UpdateProductStockAsync(int productId, byte newStock)
        {
            try
            {
                await _productRepository.UpdateProductStockAsync(productId, newStock);
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün stoğu güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<ProductDto> GetProductDetailsAsync(int productId)
        {
            try
            {
                var product = await _productRepository.GetProductDetailsAsync(productId);
                if (product == null)
                {
                    throw new Exception("Ürün bulunamadı.");
                }

                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Details = product.Details,
                    StockAmount = product.StockAmount,
                    CategoryId = product.CategoryId,
                    SellerId = product.SellerId,
                    CreatedAt = product.CreatedAt,
                    Enabled = product.Enabled
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün detayları getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            try
            {
                var products = await _productRepository.SearchProductsAsync(searchTerm);
                return products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Details = p.Details,
                    StockAmount = p.StockAmount,
                    CategoryId = p.CategoryId,
                    SellerId = p.SellerId,
                    CreatedAt = p.CreatedAt,
                    Enabled = p.Enabled
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ürünler aranırken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IEnumerable<ProductDto>> FilterProductsAsync(string category, decimal minPrice, decimal maxPrice)
        {
            try
            {
                var products = await _productRepository.FilterProductsAsync(category, minPrice, maxPrice);
                return products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Details = p.Details,
                    StockAmount = p.StockAmount,
                    CategoryId = p.CategoryId,
                    SellerId = p.SellerId,
                    CreatedAt = p.CreatedAt,
                    Enabled = p.Enabled
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ürünler filtrelenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task DeactivateProductAsync(int productId)
        {
            try
            {
                await _productRepository.DeactivateProductAsync(productId);
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün pasif hale getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task AddProductCommentAsync(ProductCommentDto commentDto)
        {
            try
            {
                var comment = new ProductComment
                {
                    ProductId = commentDto.ProductId,
                    UserId = commentDto.UserId,
                    Text = commentDto.Text,
                    StarCount = commentDto.StarCount,
                    IsConfirmed = commentDto.IsConfirmed,
                    CreatedAt = DateTime.Now
                };

                await _productRepository.AddProductCommentAsync(comment);
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün yorumu eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task ApproveProductCommentAsync(int commentId)
        {
            try
            {
                await _productRepository.ApproveProductCommentAsync(commentId);
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün yorumu onaylanırken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IEnumerable<ProductCommentDto>> FilterProductCommentsAsync(int productId, int starCount, bool? isConfirmed)
        {
            try
            {
                var comments = await _productRepository.FilterProductCommentsAsync(productId, starCount, isConfirmed);
                return comments.Select(c => new ProductCommentDto
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    UserId = c.UserId,
                    Text = c.Text,
                    StarCount = c.StarCount,
                    IsConfirmed = c.IsConfirmed,
                    CreatedAt = c.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ürün yorumları filtrelenirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
