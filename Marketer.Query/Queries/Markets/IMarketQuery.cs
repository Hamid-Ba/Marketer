using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Query.Queries.Markets
{
    public interface IMarketQuery
    {
        Task<IEnumerable<MarketQueryVM>> GetAllBy(long visitorId);
    }
}