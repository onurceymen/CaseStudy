using CaseStudyData.Repository;
using CaseStudyEntity.Entity;

namespace CaseStudyBusiness.Abstract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order> GetOrderDetailsAsync(int orderId);
        Task CancelOrderAsync(int orderId);
    }
}
