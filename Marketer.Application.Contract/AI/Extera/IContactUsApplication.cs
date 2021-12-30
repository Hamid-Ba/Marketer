using Framework.Application;
using Marketer.Application.Contract.ViewModels.Extera;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Application.Contract.AI.Extera
{
    public interface IContactUsApplication
    {
        Task<string> GetMessageBy(long id);
        Task<IEnumerable<ContactUsVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<OperationResult> Send(CreateContactUs command);
    }
}
