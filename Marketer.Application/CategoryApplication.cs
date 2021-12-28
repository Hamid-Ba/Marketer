using Framework.Application;
using Marketer.Application.Contract.AI.Products;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using Marketer.Domain.RI.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly ICategoryRepository _categoryRepository;

        public async Task<OperationResult> Create(CreateCategoryVM command)
        {
            OperationResult result = new();

            if (_categoryRepository.Exists(c => c.Name == command.Name || c.Slug == command.Slug))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var category = new Category(command.Name, command.Description, command.KeyWords, command.MetaDescription, command.Slug.Slugify());

            await _categoryRepository.AddEntityAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var category = await _categoryRepository.GetEntityByIdAsync(id);
            if (category is null) return result.Failed(ApplicationMessage.NotExist);

            category.Delete();
            await _categoryRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<CategoryVM>> GetAll() => await _categoryRepository.GetAll();

        public async Task<OperationResult> Edit(EditCategoryVM command)
        {
            OperationResult result = new();

            var category = await _categoryRepository.GetEntityByIdAsync(command.Id);

            if (category is null) return result.Failed(ApplicationMessage.NotExist);
            if (_categoryRepository.Exists(c => (c.Name == command.Name || c.Slug == command.Slug) && c.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            category.Edit(command.Name, command.Description, command.KeyWords, command.MetaDescription, command.Slug.Slugify());
            await _categoryRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditCategoryVM> GetDetailForEditBy(long id) => await _categoryRepository.GetDetailForEditBy(id);
    }
}
