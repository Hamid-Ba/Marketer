using Framework.Application;
using Marketer.Application.Contract.ViewModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Products
{
    public interface IProductApplication
    {
        Task<IEnumerable<ProductVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<EditProductVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditProductVM command);
        Task<OperationResult> Create(CreateProductVM command);
        Task<IEnumerable<SelectProductVM>> GetAllForSelection();
    }
}
