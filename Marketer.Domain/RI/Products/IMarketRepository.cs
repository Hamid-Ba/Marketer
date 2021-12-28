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
        Task<EditMarketVM> GetDetailForEditBy(long id);
    }
}