using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Query.Queries.Products
{
    public interface IProductQuery
    {
        Task<ProductQueryVM> GetBy(string slug);
        Task<IEnumerable<ProductQueryVM>> GetAll(ProductSort sort,string search,string catSlug,string brandSlug, int take = 0);
    }
}
