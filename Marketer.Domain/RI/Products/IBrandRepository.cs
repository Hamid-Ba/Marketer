using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Products
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<IEnumerable<BrandVM>> GetAll();
        Task<EditBrandVM> GetDetailForEditBy(long id);
    }
}
