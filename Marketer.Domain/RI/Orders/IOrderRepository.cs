using Framework.Domain;
using Marketer.Domain.Entities.Orders;

namespace Marketer.Domain.RI.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}