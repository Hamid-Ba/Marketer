using Framework.Application;
using Marketer.Application.Contract.ViewModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Products
{
    public interface IMarketApplication
    {
        Task<IEnumerable<MarketVM>> GetAll();
        Task<IEnumerable<MarketVM>> GetAll(long visitorId);
        Task<OperationResult> Delete(long id);
        Task<EditMarketVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditMarketVM command);
        Task<OperationResult> Create(CreateMarketVM command);
        Task<OperationResult> DoesMarketBelongToVisitor(long id, long visitorId);
    }
}