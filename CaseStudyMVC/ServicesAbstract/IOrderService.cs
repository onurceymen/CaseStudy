using CaseStudyMVC.Models;

namespace CaseStudyMVC.ServicesAbstract
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetOrdersAsync(string userId);
        Task<OrderViewModel> GetOrderDetailsAsync(int orderId);
        Task<bool> CreateOrderAsync(CreateOrderViewModel model);
    }
}
