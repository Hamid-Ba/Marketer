using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Extera;
using Marketer.Domain.Entities.Extera;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Extera
{
    public interface IContactUsRepository : IRepository<ContactUs>
    {
        Task<string> GetMessageBy(long id);
        Task<IEnumerable<ContactUsVM>> GetAll();
    }
}
