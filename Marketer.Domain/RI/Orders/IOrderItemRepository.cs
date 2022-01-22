using Framework.Domain;
using Marketer.Domain.Entities.Orders;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Orders
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<OrderItem> GetBy(long id);
    }
}