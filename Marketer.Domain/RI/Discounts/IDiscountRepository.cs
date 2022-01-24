using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Discounts;
using Marketer.Domain.Entities.Discounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Discounts
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<Discount> GetBy(long productId);
        Task<IEnumerable<DiscountVM>> GetAll();
        Task<EditDiscountVM> GetDetailForEditBy(long id);
    }
}