using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Products
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<CategoryVM>> GetAll();
        Task<EditCategoryVM> GetDetailForEditBy(long id);
        Task<IEnumerable<SelectCategoryVM>> GetAllForSelection();
    }
}
