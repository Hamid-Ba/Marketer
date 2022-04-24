using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Application;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using Marketer.Domain.RI.Products;

namespace Marketer.Application
{
    public class PackageTypeApplication : IPackageTypeApplication
	{
        private readonly IPackageTypeRepository _packageTypeRepository;

        public PackageTypeApplication(IPackageTypeRepository packageTypeRepository) => _packageTypeRepository = packageTypeRepository;
		
        public async Task<OperationResult> Create(CreatePackageTypeVM command)
        {
            OperationResult result = new();

            if (_packageTypeRepository.Exists(p => p.Title == command.Title))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var package = new PackageType(command.Title);

            await _packageTypeRepository.AddEntityAsync(package);
            await _packageTypeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(DeletePackageTypeVM command)
        {
            OperationResult result = new();

            var package = await _packageTypeRepository.GetEntityByIdAsync(command.Id);
            if (package is null) return result.Failed(ApplicationMessage.NotExist);

            package.Delete();
            await _packageTypeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditPackageTypeVM command)
        {
            OperationResult result = new();

            var package = await _packageTypeRepository.GetEntityByIdAsync(command.Id);

            if (package is null) return result.Failed(ApplicationMessage.NotExist);
            if (_packageTypeRepository.Exists(p => p.Title == command.Title && p.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            package.Edit(command.Title);
            await _packageTypeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<PackageTypeVM>> GetAll() => await _packageTypeRepository.GetAll();

        public async Task<PackageTypeVM> GetBy(long id) => await _packageTypeRepository.GetBy(id);

        public async Task<EditPackageTypeVM> GetDetailForEditBy(long id) => await _packageTypeRepository.GetDetailForEditBy(id);
    }
}