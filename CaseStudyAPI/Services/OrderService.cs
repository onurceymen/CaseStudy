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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(OrderDto orderDto)
        {
            try
            {
                var order = new Order
                {
                    UserId = orderDto.UserId,
                    OrderCode = orderDto.OrderCode,
                    Address = orderDto.Address,
                    CreatedAt = DateTime.Now
                };

                await _orderRepository.AddAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception("Sipariş oluşturulurken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
                return orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    OrderCode = o.OrderCode,
                    Address = o.Address,
                    CreatedAt = o.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Siparişler getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<OrderDto> GetOrderDetailsAsync(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderDetailsAsync(orderId);
                if (order == null)
                {
                    throw new Exception("Sipariş bulunamadı.");
                }

                return new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderCode = order.OrderCode,
                    Address = order.Address,
                    CreatedAt = order.CreatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Sipariş detayları getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task CancelOrderAsync(int orderId)
        {
            try
            {
                await _orderRepository.CancelOrderAsync(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception("Sipariş iptal edilirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
