using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Account;
using Marketer.Domain.Entities.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Account
{
    public interface IOperatorRepository : IRepository<Operator>
    {
        Task<Operator> GetBy(string mobile);
       Task<IEnumerable<OperatorVM>> GetAll();
       Task<EditOperatorVM> GetDetailForEditBy(long id);
    }
}
