using CaseStudyData.Repository;
using CaseStudyEntity.Entity;


namespace CaseStudyBusiness.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task<Order> GetOrderDetailsAsync(int orderId);
        Task CancelOrderAsync(int orderId);

    }
}
