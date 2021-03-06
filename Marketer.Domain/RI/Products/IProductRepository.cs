using Framework.Domain;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Domain.RI.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetBy(string slug);
        Task<IEnumerable<ProductVM>> GetAll();
        Task<EditProductVM> GetDetailForEditBy(long id);
        Task<IEnumerable<SelectProductVM>> GetAllForSelection();
        StatusCheckVM CheckStock(CheckCartItemCountVM command);
    }
}