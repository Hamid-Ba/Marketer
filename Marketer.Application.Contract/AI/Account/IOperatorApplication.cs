using Framework.Application;
using Marketer.Application.Contract.ViewModels.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Account
{
    public interface IOperatorApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<OperatorVM>> GetAll();
        Task<EditOperatorVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditOperatorVM command);
        Task<OperationResult> Login(LoginOperatorVM command);
        Task<OperationResult> Create(CreateOperatorVM command);
    }
}
