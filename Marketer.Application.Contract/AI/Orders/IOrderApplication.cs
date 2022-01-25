using Framework.Application;
using Marketer.Application.Contract.ViewModels.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Orders
{
    public interface IOrderApplication
    {
        Task<IEnumerable<OrderVM>> GetAll();
        Task<bool> IsThereOpenOrder(long visitorId);
        Task<int> CountOfProductInItem(long orderItemId);
        Task<OperationResult> PlaceOrder(OrderVM command);
        Task<OperationResult> CreateOrder(long visitorId);
        Task<IEnumerable<OrderVM>> GetAllBy(long visitorId);
        Task<IEnumerable<OrderItemVM>> GetOrderDetails(long orderId);
        Task<ChangeStatusOrderVM> GetDetailForChangeStatusBy(long id);
        Task<OperationResult> ChangeStatus(ChangeStatusOrderVM command);
        Task<OperationResult> UpdateCountOfItems(long[] itemsId, int[] quantity);
        Task<OperationResult> DeleteOrderItemBy(long visitorId,long orderItemId);
        Task<IEnumerable<OrderItemVM>> GetOrderDetailsBy(long orderId,long visitorId);
        Task<OperationResult> AddProductToOpenOrder(long visitorId,string productSlug);
    }
}
