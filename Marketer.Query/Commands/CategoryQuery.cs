using Marketer.Infrastructure.EfCore;
using Marketer.Query.Queries.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Query.Commands
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly MarketerContext _context;

        public CategoryQuery(MarketerContext context) => _context = context;

        public async Task<IEnumerable<CategoryQueryVM>> GetAll() => await _context.Categories.Select(c => new CategoryQueryVM
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug
        }).AsNoTracking().ToListAsync();
        
    }
}
