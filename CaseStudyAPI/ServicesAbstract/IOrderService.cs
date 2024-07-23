using CaseStudyBusiness.Dtos;

namespace CaseStudyAPI.ServicesAbstract
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderDto order);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId);
        Task<OrderDto> GetOrderDetailsAsync(int orderId);
        Task CancelOrderAsync(int orderId);
    }
}
