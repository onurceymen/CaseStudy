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
    public class CartService : ICartService
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItemsByUserIdAsync(string userId)
        {
            try
            {
                var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(userId);
                return cartItems.Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    UserId = ci.UserId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    CreatedAt = ci.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Sepet öğeleri getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<bool> AddCartItemAsync(CartItemDto cartItemDto)
        {
            try
            {
                var cartItem = new CartItem
                {
                    UserId = cartItemDto.UserId,
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity,
                    CreatedAt = DateTime.Now
                };

                await _cartItemRepository.AddAsync(cartItem);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Sepet öğesi eklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItem == null)
                {
                    throw new Exception("Sepet öğesi bulunamadı.");
                }

                _cartItemRepository.Remove(cartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Sepet öğesi kaldırılırken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task UpdateCartItemQuantityAsync(int cartItemId, byte newQuantity)
        {
            try
            {
                await _cartItemRepository.UpdateCartItemQuantityAsync(cartItemId, newQuantity);
            }
            catch (Exception ex)
            {
                throw new Exception("Sepet öğesi miktarı güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<CartItemDto> GetCartItemByUserAndProductIdAsync(string userId, int productId)
        {
            try
            {
                var cartItem = await _cartItemRepository.GetCartItemByUserAndProductIdAsync(userId, productId);
                if (cartItem == null)
                {
                    throw new Exception("Kullanıcı ve ürün için sepet öğesi bulunamadı.");
                }

                return new CartItemDto
                {
                    Id = cartItem.Id,
                    UserId = cartItem.UserId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    CreatedAt = cartItem.CreatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Sepet öğesi getirilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
