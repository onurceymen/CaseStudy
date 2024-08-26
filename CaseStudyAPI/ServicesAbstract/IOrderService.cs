using CaseStudyBusiness.Dtos;

namespace CaseStudyAPI.ServicesAbstract
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderCreateDto order, int userId);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId);
        Task<OrderDetailsDto> GetOrderDetailsAsync(int orderId);
        Task CancelOrderAsync(int orderId);
    }
}
