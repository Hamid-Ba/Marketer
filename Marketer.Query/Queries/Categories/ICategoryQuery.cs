using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketer.Query.Queries.Categories
{
    public interface ICategoryQuery
    {
        Task<IEnumerable<CategoryQueryVM>> GetAll();
    }
}