using Marketer.Infrastructure.EfCore;
using Marketer.Query.Queries.Brands;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Query.Commands
{
    public class BrandQuery : IBrandQuery
    {
        private readonly MarketerContext _context;

        public BrandQuery(MarketerContext context) => _context = context;

        public async Task<IEnumerable<BrandQueryVM>> GetAll() => await _context.Brands.Select(b => new BrandQueryVM
        {
            Id = b.Id,
            Name = b.Name,
            Picture = b.Picture,
            PictureAlt = b.PictureAlt,
            PictureTitle = b.PictureTitle,
            Slug = b.Slug
        }).AsNoTracking().ToListAsync();
        
    }
}