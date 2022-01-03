using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Query.Queries.Products
{
    public interface IProductQuery
    {
        Task<IEnumerable<ProductQueryVM>> GetAll(int take = 0);
    }
}
