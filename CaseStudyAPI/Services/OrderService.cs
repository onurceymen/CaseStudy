using CaseStudyAPI.ServicesAbstract;
using CaseStudyBusiness.Abstract;
using CaseStudyBusiness.Dtos;
using CaseStudyEntity.Entity;

namespace CaseStudyAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(OrderCreateDto orderDto, int userId)
        {
            try
            {
                var order = new Order
                {
                    UserId = userId,
                    OrderCode = Guid.NewGuid().ToString(), // Benzersiz bir sipariş kodu oluşturma
                    Address = orderDto.Address,
                    CreatedAt = DateTime.Now,
                    OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = 0 // Fiyat bilgisinin daha sonra alınacağını varsayıyoruz
                    }).ToList()
                };

                await _orderRepository.AddAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception("Sipariş oluşturulurken bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId)
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

        public async Task<OrderDetailsDto> GetOrderDetailsAsync(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderDetailsAsync(orderId);
                if (order == null)
                {
                    throw new Exception("Sipariş bulunamadı.");
                }

                return new OrderDetailsDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderCode = order.OrderCode,
                    Address = order.Address,
                    CreatedAt = order.CreatedAt,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
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
