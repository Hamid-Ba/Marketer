using Framework.Application;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Orders
{
    public interface IOrderApplication
    {
        Task<bool> IsThereOpenOrder(long visitorId);
        Task<OperationResult> CreateOrder(long visitorId);
        Task<OperationResult> DeleteOrderItemBy(long visitorId,long orderItemId);
        Task<OperationResult> AddProductToOpenOrder(long visitorId,string productSlug);
    }
}
