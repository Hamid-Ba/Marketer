using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Products
{
    public interface IMarketRepository : IRepository<Market>
    {
        Task<IEnumerable<MarketVM>> GetAll();
        Task<IEnumerable<MarketVM>> GetAll(long visitorId);
        Task<EditMarketVM> GetDetailForEditBy(long id);
        Task<MarketVM> GetBy(long id);
    }
}