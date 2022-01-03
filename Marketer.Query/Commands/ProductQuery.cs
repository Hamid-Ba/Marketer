using Marketer.Infrastructure.EfCore;
using Marketer.Query.Queries.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Query.Commands
{
    public class ProductQuery : IProductQuery
    {
        private readonly MarketerContext _context;

        public ProductQuery(MarketerContext context) => _context = context;

        public async Task<IEnumerable<ProductQueryVM>> GetAll(int take = 0)
        {
            var products =  _context.Products.Select(p => new ProductQueryVM
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                ConsumerPrice = p.ConsumerPrice,
                PurchasePrice = p.PurchacePrice,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                CreationDate = p.CreationDate
            }).AsNoTracking().OrderByDescending(p => p.CreationDate).AsQueryable();


            if(take > 0)  return await products.Take(take).ToListAsync();

            return await products.ToListAsync();
        }
    }
}