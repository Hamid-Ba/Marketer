using Framework.Application;
using Marketer.Application.Contract.ViewModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Products
{
    public interface ICityApplication
    {
        Task<IEnumerable<CityVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<EditCityVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditCityVM command);
        Task<OperationResult> Create(CreateCityVM command);
    }
}