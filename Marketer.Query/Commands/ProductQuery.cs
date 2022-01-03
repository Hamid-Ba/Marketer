using Framework.Application;
using Marketer.Infrastructure.EfCore;
using Marketer.Query.Queries.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketer.Query.Commands
{
    public class ProductQuery : IProductQuery
    {
        private readonly MarketerContext _context;

        public ProductQuery(MarketerContext context) => _context = context;

        public async Task<ProductQueryVM> GetBy(string slug) => await _context.Products.Include(b => b.Brand).Include(c => c.Category)
            .Where(p => p.Slug == slug).Select(p => new ProductQueryVM
            {
                Id = p.Id,
                BrandId = p.BrandId,
                BrandName = p.Brand.Name,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Code = p.Code,
                Slug = p.Slug,
                Title = p.Title,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                ConsumerPrice = p.ConsumerPrice,
                PurchasePrice = p.PurchacePrice,
                Count = p.Count,
                IsStock = p.IsStock,
                EachBoxCount = p.EachBoxCount,
                ExpiredDate = p.ExpiredDate.ToFarsi(),
                Keywords = p.Keywords,
                MetaDescription = p.MetaDescription,
                Weight = p.Weight,
                Description = p.Description,
            }).FirstOrDefaultAsync();

        public async Task<IEnumerable<ProductQueryVM>> GetAll(int take = 0)
        {
            var discounts = await _context.Discounts.Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now).
              Select(d => new
              {
                  Id = d.Id,
                  ProductId = d.ProductId,
                  Rate = d.DiscountRate
              }).ToListAsync();


            var products =await _context.Products.Select(p => new ProductQueryVM
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
            }).AsNoTracking().OrderByDescending(p => p.CreationDate).ToListAsync();

            products.ForEach(d => d.DiscountRate = discounts.FirstOrDefault(q => q.ProductId == d.Id)?.Rate);

            if (take > 0) return products.Take(take);

            return products;
        }

    }
}