using OnlineBookstoreMS.Models.Entity;

namespace OnlineBookstoreMS.Interface
{
    public interface IOrderService
    {
        Task<Order> PlaceOrder(int userId);
        Task<Order> GetOrderById(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
    }

}