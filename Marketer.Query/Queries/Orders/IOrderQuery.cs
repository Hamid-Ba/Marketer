using System.Threading.Tasks;

namespace Marketer.Query.Queries.Orders
{
    public interface IOrderQuery
    {
        Task<OrderQueryVM> GetOpenOrder(long visitorId);
        Task<FinalBasketVM> CalculateOrder(long visitorId);
    }
}