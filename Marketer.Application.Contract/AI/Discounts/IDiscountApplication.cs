using Framework.Application;
using Marketer.Application.Contract.ViewModels.Discounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Discounts
{
    public interface IDiscountApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<DiscountVM>> GetAll();
        Task<EditDiscountVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditDiscountVM command);
        Task<OperationResult> Create(CreateDiscountVM command);
    }
}