using Framework.Domain;
using Marketer.Domain.Entities.Orders;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOpenOrder(long visitorId);
        Task<bool> IsThereOpenOrder(long visitorId);
    }
}