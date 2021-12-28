using Framework.Application;
using Marketer.Application.Contract.ViewModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Products
{
    public interface ICategoryApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<CategoryVM>> GetAll();
        Task<EditCategoryVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditCategoryVM command);
        Task<OperationResult> Create(CreateCategoryVM command);
    }
}