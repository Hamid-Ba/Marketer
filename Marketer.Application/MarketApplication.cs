﻿using Framework.Application;
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

        public MarketApplication(IMarketRepository marketRepository)
        {
            _marketRepository = marketRepository;
        }

        public async Task<OperationResult> Create(CreateMarketVM command)
        {
            OperationResult result = new();

            if (_marketRepository.Exists(m => m.MobilePhone == command.MobilePhone))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var market = new Market(command.VisitorId, command.Name, command.Owner, command.MobilePhone);

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

        public async Task<OperationResult> Edit(EditMarketVM command)
        {
            OperationResult result = new();

            var market = await _marketRepository.GetEntityByIdAsync(command.Id);
           
            if (market is null) return result.Failed(ApplicationMessage.NotExist);
            if (_marketRepository.Exists(m => m.MobilePhone == command.MobilePhone && m.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            market.Edit(command.Name, command.Owner, command.MobilePhone);
            await _marketRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<MarketVM>> GetAll() => await _marketRepository.GetAll();

        public async Task<EditMarketVM> GetDetailForEditBy(long id) => await _marketRepository.GetDetailForEditBy(id);
    }
}