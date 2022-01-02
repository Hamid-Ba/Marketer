using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Query.Queries.Brands
{
    public interface IBrandQuery
    {
        Task<IEnumerable<BrandQueryVM>> GetAll();
    }
}
