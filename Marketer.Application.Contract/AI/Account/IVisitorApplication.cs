using Framework.Application;
using Marketer.Application.Contract.ViewModels.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Account
{
    public interface IVisitorApplication
    {
        Task<IEnumerable<VisitorVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<OperationResult> BlockProcess(long id);
        Task<EditVisitorVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditVisitorVM command);
        Task<OperationResult> Login(LoginVisitorVM command);
        Task<OperationResult> Create(CreateVisitorVM command);
    }
}