﻿using Framework.Application;
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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly MarketerContext _context;

        public ProductRepository(MarketerContext context) : base(context) => _context = context;

        public async Task<IEnumerable<ProductVM>> GetAll() => await _context.Products.Include(s => s.Brand).Include(c => c.Category).Select(
                p => new ProductVM()
                {
                    Id = p.Id,
                    Title = p.Title,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    BrandId = p.BrandId,
                    BrandName = p.Brand.Name,
                    OrderCount = p.OrderCount,
                    Code = p.Code,
                    ConsumerPrice = p.ConsumerPrice,
                    PurchasePrice = p.PurchacePrice,
                    Profit = p.Profit,
                    EachBoxCount = p.EachBoxCount,
                    Keywords = p.Keywords,
                    MetaDescription = p.MetaDescription,
                    Picture = p.Picture,
                    Slug = p.Slug,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Count = p.Count,
                    ExpiredDate = p.ExpiredDate.ToFarsi(),
                    IsStock = p.IsStock,
                    Weight = p.Weight
                }).AsNoTracking().ToListAsync();

        public async Task<EditProductVM> GetDetailForEditBy(long id) => await _context.Products.Select(p => new EditProductVM
        {
            Id = p.Id,
            Title = p.Title,
            CategoryId = p.CategoryId,
            BrandId = p.BrandId,
            Code = p.Code,
            ConsumerPrice = p.ConsumerPrice,
            PurchacePrice = p.PurchacePrice,
            EachBoxCount = p.EachBoxCount,
            Keywords = p.Keywords,
            MetaDescription = p.MetaDescription,
            PictureName = p.Picture,
            Slug = p.Slug,
            PictureAlt = p.PictureAlt,
            PictureTitle = p.PictureTitle,
            Count = p.Count,
            ExpiredDate = p.ExpiredDate.ToFarsi(),
            Weight = p.Weight
        }).FirstOrDefaultAsync(p => p.Id == id);
    }
}