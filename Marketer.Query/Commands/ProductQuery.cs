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

        public async Task<ProductQueryVM> GetBy(string slug) => await _context.Products
            .Include(b => b.Brand)
            .Include(c => c.Category)
            .Include(p => p.PackageType)
            .Where(p => p.Slug == slug).Select(p => new ProductQueryVM
            {
                Id = p.Id,
                BrandId = p.BrandId,
                BrandName = p.Brand.Name,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                CategorySlug = p.Category.Slug,
                PackageTypeId = p.PackageTypeId,
                PackageTypeTitle = p.PackageType.Title,
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
                //EachBoxCount = p.EachBoxCount,
                ExpiredDate = p.ExpiredDate.ToFarsi(),
                Keywords = p.Keywords,
                MetaDescription = p.MetaDescription,
                //Weight = p.Weight,
                PackageValue = p.PackageValue,
                Description = p.Description,
            }).FirstOrDefaultAsync();

        public async Task<IEnumerable<ProductQueryVM>> GetAll(ProductSort sort, string search, string catSlug,string brandSlug, int take = 0)
        {
            var discounts = await _context.Discounts.Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now).
              Select(d => new
              {
                  Id = d.Id,
                  ProductId = d.ProductId,
                  Rate = d.DiscountRate
              }).ToListAsync();


            var products = await _context.Products.Include(c => c.Category).Include(b => b.Brand).Select(p => new ProductQueryVM
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                BrandSlug = p.Brand.Slug,
                CategorySlug = p.Category.Slug,
                ConsumerPrice = p.ConsumerPrice,
                PurchasePrice = p.PurchacePrice,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                CreationDate = p.CreationDate
            }).AsNoTracking().OrderByDescending(p => p.CreationDate).ToListAsync();

            products.ForEach(d => d.DiscountRate = discounts.FirstOrDefault(q => q.ProductId == d.Id)?.Rate);

            if (!string.IsNullOrWhiteSpace(search))
                products = products.Where(p => p.Title.Contains(search)).ToList();

            if (!string.IsNullOrWhiteSpace(catSlug) && catSlug != 0.ToString())
                products = products.Where(p => p.CategorySlug == catSlug).ToList();

            if (!string.IsNullOrWhiteSpace(brandSlug) && brandSlug != 0.ToString())
                products = products.Where(p => p.BrandSlug == brandSlug).ToList();

            switch (sort)
            {
                case ProductSort.Newest:
                    products = products.OrderBy(p => p.CreationDate).ToList();
                    break;
                case ProductSort.Oldest:
                    products = products.OrderByDescending(p => p.CreationDate).ToList();
                    break;
                case ProductSort.Cheapest:
                    products = products.OrderByDescending(p => p.PurchasePrice).ToList();
                    break;
                case ProductSort.Expencive:
                    products = products.OrderBy(p => p.PurchasePrice).ToList();
                    break;
            }

            if (take > 0) return products.Take(take);

            return products;
        }

    }
}