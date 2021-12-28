using Framework.Infrastructure;
using Marketer.Application.Contract.ViewModels.Products;
using Marketer.Domain.Entities.Products;
using Marketer.Domain.RI.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Infrastructure.EfCore.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly MarketerContext _context;

        public CategoryRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<CategoryVM>> GetAll() => await _context.Categories.Select(c => new CategoryVM()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Slug = c.Slug
        }).AsNoTracking().ToListAsync();

        public async Task<EditCategoryVM> GetDetailForEditBy(long id) => await _context.Categories.Select(c => new EditCategoryVM
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            KeyWords = c.KeyWords,
            Description = c.Description,
            MetaDescription = c.MetaDescription
        }).FirstOrDefaultAsync(c => c.Id == id);

    }
}