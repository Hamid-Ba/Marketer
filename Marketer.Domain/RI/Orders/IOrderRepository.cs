using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Orders;
using Marketer.Domain.Entities.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOpenOrder(long visitorId);
        Task<bool> IsThereOpenOrder(long visitorId);
        Task<IEnumerable<OrderVM>> GetAll();
        Task<ChangeStatusOrderVM> GetDetailForChangeStatusBy(long id);
        Task<IEnumerable<OrderVM>> GetAllBy(long visitorId);
    }
}