using Framework.Application;
using Marketer.Application.Contract.ViewModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Products
{
    public interface IBrandApplication
    {
        Task<IEnumerable<BrandVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<EditBrandVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditBrandVM command);
        Task<OperationResult> Create(CreateBrandVM command);
        Task<IEnumerable<SelectBrandVM>> GetAllForSelection();
    }
}
