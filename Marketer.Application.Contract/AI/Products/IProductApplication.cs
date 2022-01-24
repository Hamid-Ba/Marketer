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
        Task<OperationResult> IsProductExistInStock(long id);
        Task<OperationResult> Create(CreateProductVM command);
        StatusCheckVM CheckStock(CheckCartItemCountVM command);
        Task<IEnumerable<SelectProductVM>> GetAllForSelection();
        Task<OperationResult> IsProductExistInStock(string slug);
        Task<OperationResult> GetBackProductCount(long id, int count);
        Task<OperationResult> IsCountSatisfyStock(long[] ids, int[] count);
    }
}