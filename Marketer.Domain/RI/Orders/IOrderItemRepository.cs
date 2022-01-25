using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Orders;
using Marketer.Domain.Entities.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Orders
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<OrderItem> GetBy(long id);
        Task<IEnumerable<OrderItemVM>> GetOrderDetails(long orderId);
        Task<IEnumerable<OrderItemVM>> GetOrderDetails(long orderId, long visitorId);
    }
}