using Framework.Application;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using Marketer.Domain.RI.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class BrandApplication : IBrandApplication
    {
        private readonly IBrandRepository _brandRepository;

        public async Task<OperationResult> Create(CreateBrandVM command)
        {
            OperationResult result = new();

            if (_brandRepository.Exists(b => b.Name == command.Name))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var brand = new Brand(command.Name,command.KeyWords,command.MetaDescription,command.Slug.Slugify());

            await _brandRepository.AddEntityAsync(brand);
            await _brandRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var brand = await _brandRepository.GetEntityByIdAsync(id);
            if (brand is null) return result.Failed(ApplicationMessage.NotExist);

            brand.Delete();
            await _brandRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditBrandVM command)
        {
            OperationResult result = new();

            var brand = await _brandRepository.GetEntityByIdAsync(command.Id);

            if (brand is null) return result.Failed(ApplicationMessage.NotExist);
            if (_brandRepository.Exists(b => b.Name == command.Name && b.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            brand.Edit(command.Name,command.KeyWords,command.MetaDescription,command.Slug.Slugify());
            await _brandRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<BrandVM>> GetAll() => await _brandRepository.GetAll();

        public async Task<EditBrandVM> GetDetailForEditBy(long id) => await _brandRepository.GetDetailForEditBy(id);
    }
}