using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Query.Queries.Products
{
    public interface IProductQuery
    {
        Task<ProductQueryVM> GetBy(string slug);
        Task<IEnumerable<ProductQueryVM>> GetAll(int take = 0);
    }
}
