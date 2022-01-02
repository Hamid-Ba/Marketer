using Framework.Application;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using Marketer.Domain.RI.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class CityApplication : ICityApplication
    {
        private readonly ICityRepository _cityRepository;

        public CityApplication(ICityRepository cityRepository) => _cityRepository = cityRepository;

        public async Task<OperationResult> Create(CreateCityVM command)
        {
            OperationResult result = new();

            if (_cityRepository.Exists(p => p.Name == command.Name)) return result.Failed(ApplicationMessage.DuplicatedModel);

            var city = new City(command.Name);

            await _cityRepository.AddEntityAsync(city);
            await _cityRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var City = await _cityRepository.GetEntityByIdAsync(id);
            if (City is null) return result.Failed(ApplicationMessage.NotExist);

            City.Delete();
            await _cityRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<CityVM>> GetAll() => await _cityRepository.GetAll();

        public async Task<OperationResult> Edit(EditCityVM command)
        {
            OperationResult result = new();

            var City = await _cityRepository.GetEntityByIdAsync(command.Id);

            if (City is null) return result.Failed(ApplicationMessage.NotExist);
            if (_cityRepository.Exists(p => p.Name == command.Name && p.Id != command.Id)) return result.Failed(ApplicationMessage.DuplicatedModel);

            City.Edit(command.Name);
            await _cityRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditCityVM> GetDetailForEditBy(long id) => await _cityRepository.GetDetailForEditBy(id);
    }
}