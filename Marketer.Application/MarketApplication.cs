using Framework.Application;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using Marketer.Domain.RI.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class MarketApplication : IMarketApplication
    {
        private readonly IMarketRepository _marketRepository;

        public MarketApplication(IMarketRepository marketRepository) => _marketRepository = marketRepository;

        public async Task<OperationResult> Create(CreateMarketVM command)
        {
            OperationResult result = new();

            if (_marketRepository.Exists(m => m.MobilePhone == command.MobilePhone))
                return result.Failed("این فروشگاه وجود دارد");

            var market = new Market(command.CityId,command.VisitorId, command.Name, command.Owner, command.MobilePhone);

            await _marketRepository.AddEntityAsync(market);
            await _marketRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var market = await _marketRepository.GetEntityByIdAsync(id);
            if (market is null) return result.Failed(ApplicationMessage.NotExist);

            market.Delete();
            await _marketRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> DoesMarketBelongToVisitor(long id, long visitorId)
        {
            OperationResult result = new();

            var market = await _marketRepository.GetEntityByIdAsync(id);
            if (market is null) return result.Failed(ApplicationMessage.NotExist);

            if (market.VisitorId != visitorId) return result.Failed("این فروشگاه به شما تعلق ندارد");

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditMarketVM command)
        {
            OperationResult result = new();

            var market = await _marketRepository.GetEntityByIdAsync(command.Id);
           
            if (market is null) return result.Failed(ApplicationMessage.NotExist);
            if (_marketRepository.Exists(m => m.MobilePhone == command.MobilePhone && m.Id != command.Id))
                return result.Failed("این فروشگاه وجود دارد");

            market.Edit(command.CityId,command.Name, command.Owner, command.MobilePhone);
            await _marketRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<MarketVM>> GetAll() => await _marketRepository.GetAll();

        public async Task<IEnumerable<MarketVM>> GetAll(long visitorId) => await _marketRepository.GetAll(visitorId);

        public async Task<MarketVM> GetBy(long id) => await _marketRepository.GetBy(id);

        public async Task<EditMarketVM> GetDetailForEditBy(long id) => await _marketRepository.GetDetailForEditBy(id);
    }
}