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
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly MarketerContext _context;

        public BrandRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<BrandVM>> GetAll() => await _context.Brands.Select(b => new BrandVM
        {
            Id = b.Id,
            KeyWords = b.KeyWords,
            MetaDescription = b.MetaDescription,
            Name = b.Name,
            ProductCount = 0,
            Slug = b.Slug,
        }).AsNoTracking().ToListAsync();

        public async Task<EditBrandVM> GetDetailForEditBy(long id) => await _context.Brands.Select(b => new EditBrandVM
        {
            Id = b.Id,
            KeyWords = b.KeyWords,
            MetaDescription = b.MetaDescription,
            Name = b.Name,
            Slug = b.Slug
        }).FirstOrDefaultAsync(b => b.Id == id);

    }
}